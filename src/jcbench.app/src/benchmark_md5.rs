pub mod benchmark_md5 {
	use crate::benchmark;
	use chrono;
	use md5;

	impl benchmark::Benchmark for BenchmarkMD5 {
		fn run(settings: BenchmarkSettings) -> String {
			return md5::compute(chrono::offset::Local::now().to_string()).to_string();
		}
	}
}
