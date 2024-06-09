use std::ops::Deref;

pub trait Benchmark {
	fn run(&self) -> String;

    fn get_api_version(&self) -> u32;
}

impl Benchmark for Box<dyn Benchmark> {
    fn run(&self) -> String {
        self.deref().run()
    }

    fn get_api_version(&self) -> u32 {
        self.deref().get_api_version()
    }
}