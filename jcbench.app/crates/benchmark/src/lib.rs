use std::ops::Deref;
use chrono;

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

pub fn run_benchmark<T: Benchmark>(selected_benchmark: T) -> u32 {
    const SECONDS_TO_RUN:i64 = 2;

    let start_time = chrono::offset::Local::now();

    let mut number_iterations:u32 = 0;

    while (chrono::offset::Local::now() - start_time).num_seconds() < SECONDS_TO_RUN
    {
        let _result = selected_benchmark.run();
        
        if (chrono::offset::Local::now() - start_time).num_seconds() > SECONDS_TO_RUN
        {
            break;
        }

        number_iterations = number_iterations + 1;
    }

    return number_iterations;
}