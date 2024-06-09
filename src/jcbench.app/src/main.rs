use std::env;

use chrono;
use sysinfo::System;
use serde::Serialize;

mod benchmark;
mod benchmark_md5;
mod benchmark_sha1;
mod cpuinfo;

use crate::benchmark::Benchmark;

#[derive(strum_macros::Display)]
enum Benchmarks {
    MD5,
    SHA1
}

pub struct BenchmarkSettings {
	multi_threaded: bool
}

#[derive(Serialize, Debug)]
pub struct BenchmarkRequest {
    os_name: String,
    benchmark_name: String,
    benchmark_threading_model: String,
    benchmark_api_version: u32,
    cpu_name: String,
    cpu_architecture: String,
    cpu_cores: usize,
    score: u32
}

fn run_benchmark<T: Benchmark>(selected_benchmark: T) -> u32 {
    const SECONDS_TO_RUN:i64 = 2;

    let start_time = chrono::offset::Local::now();

    let mut number_iterations:u32 = 0;

    while (chrono::offset::Local::now() - start_time).num_seconds() < SECONDS_TO_RUN
    {
        let _result = selected_benchmark.run();
        
        if (chrono::offset::Local::now() - start_time).num_seconds() > SECONDS_TO_RUN
        {
            break;
        }

        number_iterations = number_iterations + 1;
    }

    return number_iterations;
}

fn write_centered(str: String) {
    println!("{:=^50}", str);
}

fn submit_result(benchmark_result: BenchmarkRequest) -> bool {
    println!("Submitting score to the server...");
    
    let client = reqwest::blocking::Client::new();

    let response = client.post("https://www.jcbench.com/api/ResultSubmission")
        .json(&benchmark_result)
        .send().unwrap();
    
    return response.status() == 200;
}

fn parse_args(args: Vec<String>) -> (Box<dyn Benchmark>, Benchmarks) {
    let mut settings = BenchmarkSettings {
        multi_threaded: false
    };
    
    let mut benchmark_selection = Benchmarks::MD5;

    if args.len() > 1 {
        for arg in args {
            match arg.as_str() {
                "sha1" => benchmark_selection = Benchmarks::SHA1,
                "md5" => benchmark_selection = Benchmarks::MD5,
                "multithreaded" => settings.multi_threaded = true,
                _ => println!("Unexpected argument"),
            }
        }
    } else {
        println!("No arguments passed in...running defaults...");
    }

    let selected_benchmark: Box<dyn Benchmark> = match benchmark_selection {
        Benchmarks::MD5 => Box::new(benchmark_md5::BenchmarkMD5 {}),
        Benchmarks::SHA1 => Box::new(benchmark_sha1::BenchmarkSHA1 {})
    };

    return (selected_benchmark, benchmark_selection);
}

fn retrieve_sysinfo(selected_benchmark_name: Benchmarks, benchmark_result: u32) -> BenchmarkRequest {
    let mut sys = System::new_all();
    
    sys.refresh_all();

    let mut parsed_cpu_name = sys.cpus()[0].brand().to_string();

    #[cfg(target_os = "linux")]
    if parsed_cpu_name.len() == 0 {
        let cpu_info = cpuinfo::CPUInfo::new();
        
        parsed_cpu_name = cpu_info.get_key("uarch");
    }

    return BenchmarkRequest {
        cpu_cores: sys.cpus().len(),
        cpu_name: parsed_cpu_name,
        score: benchmark_result,
        os_name: env::consts::OS.to_string(),
        cpu_architecture: env::consts::ARCH.to_string(),
        benchmark_name: selected_benchmark_name.to_string(),
        benchmark_api_version: 2,
        benchmark_threading_model: "Single".to_string()
    };
}

fn main() {
    write_centered("jcBENCH 2024.5.0 (RUST Edition)".to_string());
    write_centered("(C) 2012-2024 Jarred Capellman".to_string());
    write_centered("Source code is available on https://github.com/jcapellman/jcBENCH".to_string());
    
    let args: Vec<String> = env::args().collect();

    let (selected_benchmark, selected_benchmark_name) = parse_args(args);

    let result:u32 = run_benchmark(selected_benchmark);
   
    println!("Benchmark Score: {result}");

    let benchmark_result = retrieve_sysinfo(selected_benchmark_name, result);

    println!("Operating System: {}", benchmark_result.os_name);
    
    println!("---------------");
    println!("CPU Information");
    println!("---------------");
    println!("Model: {}", benchmark_result.cpu_name);
    println!("Count: {}", benchmark_result.cpu_cores);
    println!("Architecture: {}", benchmark_result.cpu_architecture);
    println!("---------------");
    
    println!("Do you want to submit your result (y/n)?");

    let mut key = String::new();

    let _ = std::io::stdin().read_line(&mut key);

    if key.trim_end() != "y" {
        return;
    }
    
    let submission_result = submit_result(benchmark_result);

    if submission_result {
        println!("Submission was successful");
    } else {
        println!("Submission failed");
    }
}
