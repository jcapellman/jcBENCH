#[derive(strum_macros::Display)]
pub enum AvailableBenchmarks {
    MD5,
    SHA1
}

pub struct BenchmarkSettings {
	pub multi_threaded: bool,
    pub selected_benchmark: AvailableBenchmarks
}

pub fn parse_args(args: Vec<String>) -> BenchmarkSettings {
    let mut settings = BenchmarkSettings {
        multi_threaded: false,
        selected_benchmark: AvailableBenchmarks::MD5
    };
    
    if args.len() > 1 {
        for arg in args {
            match arg.as_str() {
                "sha1" => settings.selected_benchmark = AvailableBenchmarks::SHA1,
                "md5" => settings.selected_benchmark = AvailableBenchmarks::MD5,
                "multithreaded" => settings.multi_threaded = true,
                _ => println!("Unexpected argument"),
            }
        }
    } else {
        println!("No arguments passed in...running defaults...");
    }
    
    return settings;
}