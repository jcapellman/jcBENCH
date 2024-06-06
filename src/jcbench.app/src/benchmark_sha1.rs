use crate::benchmark;

use chrono;
use sha1::{Sha1, Digest};

pub struct BenchmarkSHA1 { }

impl benchmark::Benchmark for BenchmarkSHA1 {
	fn run(&self) -> String {
		let mut hasher = Sha1::new();

		hasher.update(chrono::offset::Local::now().to_string());

		let _result = hasher.finalize();

		return "True".to_string();
	}

	fn name(&self) -> String {
		return "SHA1".to_string();
	}
}
