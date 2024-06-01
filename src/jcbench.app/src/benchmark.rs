pub struct BenchmarkSettings {
	multi_threaded: bool
}

pub trait Benchmark {
	fn run(&self) -> String;
}