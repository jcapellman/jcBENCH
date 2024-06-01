pub(crate) mod benchmark {
	struct BenchmarkSettings {
		multi_threaded: bool
	}

	pub trait Benchmark {
		fn run(settings: BenchmarkSettings) -> String;
	}
}