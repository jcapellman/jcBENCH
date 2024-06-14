use chrono;
use sha1::{Sha1, Digest};

use crate::Benchmark;

pub struct BenchmarkSHA1 { }

impl Benchmark for BenchmarkSHA1 {
	fn run(&self) -> String {
		let mut hasher = Sha1::new();

		hasher.update(chrono::offset::Local::now().to_string());

		let _result = hasher.finalize();

		return "True".to_string();
	}

	fn get_api_version(&self) -> usize {
		return 2;
	}
}
