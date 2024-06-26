use chrono;
use md5;

use crate::Benchmark;
use rayon::prelude::*;
use rand::Rng;

pub struct BenchmarkMD5 { }

impl Benchmark for BenchmarkMD5 {
	fn run_single_threaded(&self) -> String {
		return format!("{:x}", md5::compute(chrono::offset::Local::now().to_string())).to_string();
	}

	fn run_multi_threaded(&self) -> String {
		let mut rng = rand::thread_rng();
		let mut random_array: Vec<u64> = (0..1000000).map(|_| rng.gen()).collect();

		random_array.par_iter_mut().for_each(|p| { println!("{:?}", md5::compute(p.to_string())); });

		return "Completed".to_string();
	}

	fn get_api_version(&self) -> usize {
		return 2;
	}
}
