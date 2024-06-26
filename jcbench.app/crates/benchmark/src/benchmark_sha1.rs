use chrono;
use sha1::{Sha1, Digest};

use crate::Benchmark;

pub struct BenchmarkSHA1 { }

impl Benchmark for BenchmarkSHA1 {
	fn run_single_threaded(&self) -> String {
		let mut hasher = Sha1::new();

		hasher.update(chrono::offset::Local::now().to_string());

		return format!("{:x}", hasher.finalize()).to_string();
	}

	fn run_multi_threaded(&self) -> String {
		todo!()
	}

	fn get_api_version(&self) -> usize {
		return 2;
	}
}
