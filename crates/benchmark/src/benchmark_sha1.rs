use chrono;
use sha1::{Sha1, Digest};

use crate::Benchmark;
use rayon::prelude::*;
use rand::Rng;

pub struct BenchmarkSHA1;

impl Benchmark for BenchmarkSHA1 {
	fn run_single_threaded(&self) -> String {
		let mut hasher = Sha1::new();

		hasher.update(chrono::offset::Local::now().to_string());

		format!("{:x}", hasher.finalize())
	}

	fn run_multi_threaded(&self) -> String {
		let mut rng = rand::thread_rng();
		let mut random_array: Vec<u64> = (0..1000000).map(|_| rng.gen()).collect();

		random_array.par_iter_mut().for_each(|p| {
			let mut hasher = Sha1::new();
			hasher.update(p.to_string());
			let _ = hasher.finalize();
		});

		"Completed".to_string()
	}

	fn get_api_version(&self) -> usize {
		2
	}
}
