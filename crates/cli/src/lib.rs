use std::env;

use benchmark_settings::parse_args;

pub fn run() {
	println!("jcBENCH {}", env!("CARGO_PKG_VERSION"));
	println!("(C) 2012-2024 Jarred Capellman");
	println!("Source code is available on https://github.com/jcapellman/jcBENCH");

    let args: Vec<String> = env::args().collect();

    let settings = parse_args(args);
    
    let (benchmark_score, benchmark_api_version) = benchmark::run_benchmark(settings.selected_benchmark.to_string(), &settings);

    let benchmark_result = benchmark_submission::get_benchmark_request(settings.selected_benchmark.to_string(), benchmark_api_version, settings, benchmark_score);

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