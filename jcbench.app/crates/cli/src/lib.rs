use std::env;

use benchmark_settings::parse_args;
use benchmark_submission::BenchmarkRequest;

pub fn run() {
	println!("jcBENCH 2024.6.0 (RUST Edition)");
	println!("(C) 2012-2024 Jarred Capellman");
	println!("Source code is available on https://github.com/jcapellman/jcBENCH");

    let args: Vec<String> = env::args().collect();

    let settings = parse_args(args);
    
    let benchmark_result = BenchmarkRequest {
        os_name: todo!(),
        benchmark_name: todo!(),
        benchmark_threading_model: todo!(),
        benchmark_api_version: todo!(),
        cpu_name: todo!(),
        cpu_architecture: todo!(),
        cpu_cores: todo!(),
        score: todo!(),
    };
    
    benchmark_submission::print_benchmark_request(&benchmark_result);
    
    println!("Do you want to submit your result with the above information (y/n)?");

    let mut key = String::new();

    let _ = std::io::stdin().read_line(&mut key);

    if key.trim_end() != "y" {
        return;
    }
    
    let submission_result = benchmark_submission::submit_benchmark_result(benchmark_result);

    if submission_result {
        println!("Submission was successful");
    } else {
        println!("Submission failed");
    }
}