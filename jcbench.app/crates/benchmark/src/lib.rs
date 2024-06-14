use std::ops::Deref;
use benchmark_md5::BenchmarkMD5;
use benchmark_sha1::BenchmarkSHA1;
use chrono;

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
    if selected_benchmark_name == "MD5" {
        return Box::new(BenchmarkMD5 {});
    } else { 
        return Box::new(BenchmarkSHA1 {});
    }
}

pub fn run_benchmark(selected_benchmark_name: String) -> (u32, usize) {
    let selected_benchmark = get_benchmark(selected_benchmark_name);
    
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

    return (number_iterations, selected_benchmark.get_api_version());
}