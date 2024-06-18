use std::ops::Deref;
use benchmark_md5::BenchmarkMD5;
use benchmark_sha1::BenchmarkSHA1;
use chrono;
use benchmark_settings::BenchmarkSettings;

mod benchmark_md5;
mod benchmark_sha1;

pub trait Benchmark {
	fn run(&self) -> String;

    fn get_api_version(&self) -> usize;
}

impl Benchmark for Box<dyn Benchmark> {
    fn run(&self) -> String {
        self.deref().run()
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

pub fn run_benchmark(selected_benchmark_name: String, settings: &BenchmarkSettings) -> (u32, usize) {
    let selected_benchmark = get_benchmark(selected_benchmark_name);

    let start_time = chrono::offset::Local::now();

    let mut number_iterations:u32 = 0;

    while (chrono::offset::Local::now() - start_time).num_seconds() < settings.seconds_to_run
    {
        let _result = selected_benchmark.run();
        
        if (chrono::offset::Local::now() - start_time).num_seconds() > settings.seconds_to_run
        {
            break;
        }

        number_iterations = number_iterations + 1;
    }

    return (number_iterations, selected_benchmark.get_api_version());
}