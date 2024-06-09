use serde::Serialize;

#[derive(Serialize, Debug)]
pub struct BenchmarkRequest {
    pub os_name: String,
    pub benchmark_name: String,
    pub benchmark_threading_model: String,
    pub benchmark_api_version: u32,
    pub cpu_name: String,
    pub cpu_architecture: String,
    pub cpu_cores: usize,
    pub score: u32
}

pub fn submit_result(benchmark_result: BenchmarkRequest) -> bool {
    println!("Submitting score to the server...");

    let client = reqwest::blocking::Client::new();

    let response = client.post("https://www.jcbench.com/api/ResultSubmission")
        .json(&benchmark_result)
        .send().unwrap();

    return response.status() == 200;
}