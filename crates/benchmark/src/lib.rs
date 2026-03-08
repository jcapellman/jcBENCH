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

fn get_benchmark(selected_benchmark_name: &str) -> Box<dyn Benchmark> {
	if selected_benchmark_name == "MD5" {
		Box::new(BenchmarkMD5)
	} else {
		Box::new(BenchmarkSHA1)
	}
}

fn run_single_threaded(selected_benchmark: &dyn Benchmark, settings: &BenchmarkSettings) -> i64 {
    let start_time = chrono::offset::Local::now();

    let mut number_iterations: i64 = 0;

    loop {
        let result = selected_benchmark.run_single_threaded();

        if result.is_empty() {
            break;
        }

        number_iterations += 1;

        if (chrono::offset::Local::now() - start_time).num_seconds() > settings.seconds_to_run {
            break;
        }
    }

    number_iterations
}

fn run_multi_threaded(selected_benchmark: &dyn Benchmark) -> i64 {
    let start_time = chrono::offset::Local::now();

    let _ = selected_benchmark.run_multi_threaded();

    (chrono::offset::Local::now() - start_time).num_seconds()
}

pub fn run_benchmark(selected_benchmark_name: &str, settings: &BenchmarkSettings) -> (i64, usize) {
    let selected_benchmark = get_benchmark(selected_benchmark_name);

    let number_iterations = if settings.multi_threaded {
        run_multi_threaded(selected_benchmark.as_ref())
    } else {
        run_single_threaded(selected_benchmark.as_ref(), settings)
    };

    (number_iterations, selected_benchmark.get_api_version())
}