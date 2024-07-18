use std::env;

use serde::Serialize;

use cpuinfo::CPUInfo;
use benchmark_settings::BenchmarkSettings;

#[derive(Serialize, Debug)]
pub struct BenchmarkRequest {
    pub os_name: String,
    pub benchmark_name: String,
    pub benchmark_threading_model: String,
    pub benchmark_api_version: usize,
    pub cpu_name: String,
    pub cpu_architecture: String,
    pub cpu_cores: usize,
    pub score: i64
}

pub fn get_benchmark_request(selected_benchmark_name: String, selected_benchmark_api: usize, benchmark_settings: BenchmarkSettings, benchmark_result: i64) -> BenchmarkRequest {
    let cpu_info = CPUInfo::get_cpu_info();

    return BenchmarkRequest {
        cpu_cores: cpu_info.num_cores,
        cpu_name: cpu_info.model_name,
        cpu_architecture: cpu_info.architecture,
        score: benchmark_result,
        os_name: env::consts::OS.to_string(),
        benchmark_name: selected_benchmark_name.to_string(),
        benchmark_api_version: selected_benchmark_api,
        benchmark_threading_model: if benchmark_settings.multi_threaded { "Multi".to_string() } else { "Single".to_string() }
    };
}

pub fn submit_benchmark_result(benchmark_request: BenchmarkRequest) -> bool {
    println!("Submitting score to the server...");

    let client = reqwest::blocking::Client::new();

    return match client.post("https://www.jcbench.com/api/ResultSubmission")
        .json(&benchmark_request)
        .send() {
            Err(error) => {
                println!("Error while submitting result: {error:?}");

                false
            },
            Ok(response) => {
                response.status() == 200
            }
        }
}

pub fn print_benchmark_request(benchmark_request: &BenchmarkRequest) {
    println!("----------------");
    println!("Benchmark Result");
    println!("----------------");
    println!("Type: {}", benchmark_request.benchmark_name);
    println!("Score: {}", benchmark_request.score);
    println!("Threading: {}", benchmark_request.benchmark_threading_model);
    println!("----------------\n");

    println!("---------------");
    println!("Machine Information");
    println!("---------------");
    println!("Model: {}", benchmark_request.cpu_name);
    println!("Count: {}", benchmark_request.cpu_cores);
    println!("Architecture: {}", benchmark_request.cpu_architecture);
    println!("Operating System: {}", benchmark_request.os_name);
    println!("---------------");
}