use std::ops::Deref;
use benchmark_md5::BenchmarkMD5;
use benchmark_sha1::BenchmarkSHA1;
use chrono;
use benchmark_settings::BenchmarkSettings;

mod benchmark_md5;
mod benchmark_sha1;

pub trait Benchmark {
	fn run_single_threaded(&self) -> String;

    fn run_multi_threaded(&self) -> String;

    fn get_api_version(&self) -> usize;
}

impl Benchmark for Box<dyn Benchmark> {
    fn run_single_threaded(&self) -> String {
        self.deref().run_single_threaded()
    }

    fn run_multi_threaded(&self) -> String {
        self.deref().run_multi_threaded()
    }

    fn get_api_version(&self) -> usize {
        self.deref().get_api_version()
    }
}

fn get_benchmark(selected_benchmark_name: String) -> Box<dyn Benchmark> {
    return if selected_benchmark_name == "MD5" {
        Box::new(BenchmarkMD5 {})
    } else {
        Box::new(BenchmarkSHA1 {})
    }
}

fn run_single_threaded(selected_benchmark: &Box<dyn Benchmark>, settings: &BenchmarkSettings) -> i64 {
    let start_time = chrono::offset::Local::now();

    let mut number_iterations:i64 = 0;

    loop
    {
        let result = selected_benchmark.run_single_threaded();

        if result.is_empty() {
            break;
        }

        number_iterations = number_iterations + 1;

        if (chrono::offset::Local::now() - start_time).num_seconds() > settings.seconds_to_run
        {
            break;
        }
    }

    return number_iterations;
}

fn run_multi_threaded(selected_benchmark: &Box<dyn Benchmark>) -> i64 {
    let start_time = chrono::offset::Local::now();

    let _ = selected_benchmark.run_multi_threaded();

    return (chrono::offset::Local::now() - start_time).num_seconds();
}

pub fn run_benchmark(selected_benchmark_name: String, settings: &BenchmarkSettings) -> (i64, usize) {
    let selected_benchmark = get_benchmark(selected_benchmark_name);

    let number_iterations:i64;

    match settings.multi_threaded {
        false => number_iterations = run_single_threaded(&selected_benchmark, settings),
        true => number_iterations = run_multi_threaded(&selected_benchmark)
    }

    return (number_iterations, selected_benchmark.get_api_version());
}