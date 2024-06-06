use std::ops::Deref;

pub trait Benchmark {
	fn run(&self) -> String;

    fn name(&self) -> String;
}

impl Benchmark for Box<dyn Benchmark> {
    fn run(&self) -> String {
        self.deref().run()
    }

    fn name(&self) -> String {
        todo!()
    }
}