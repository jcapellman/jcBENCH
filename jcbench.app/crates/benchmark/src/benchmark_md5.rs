use chrono;
use md5;

use crate::Benchmark;

pub struct BenchmarkMD5 { }

impl Benchmark for BenchmarkMD5 {
	fn run(&self) -> String {
		let _result = md5::compute(chrono::offset::Local::now().to_string());

		return "True".to_string();
	}

	fn get_api_version(&self) -> usize {
		return 2;
	}
}
