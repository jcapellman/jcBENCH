#[derive(strum_macros::Display)]
pub enum AvailableBenchmarks {
    MD5,
    SHA1
}

pub struct BenchmarkSettings {
	pub multi_threaded: bool,
    pub seconds_to_run: i64,
    pub selected_benchmark: AvailableBenchmarks
}

pub fn parse_args(args: Vec<String>) -> BenchmarkSettings {
    let mut settings = BenchmarkSettings {
        multi_threaded: false,
        seconds_to_run: 20,
        selected_benchmark: AvailableBenchmarks::MD5
    };

    if args.len() >= 3 {
        let mut x = 1;

        while x < args.len() {
            match args[x].as_str() {
                "benchmark" => {
                    match args[x+1].as_str() {
                        "sha1" => settings.selected_benchmark = AvailableBenchmarks::SHA1,
                        "md5" => settings.selected_benchmark = AvailableBenchmarks::MD5,
                        _ => eprintln!("Invalid benchmark argument...using default"),
                    }
                },
                "duration" => {
                    settings.seconds_to_run = i64::from_str_radix(args[x+1].as_str(), 10).unwrap();
                },
                "threading" => {
                    match args[x+1].as_str() {
                        "single" => settings.multi_threaded = false,
                        "multi" => settings.multi_threaded = true,
                        _ => println!("Invalid threading argument..using default"),
                    }
                },
                _ => println!("Unexpected argument ({})", args[x].as_str()),
            }

            x += 2;
        }
    } else {
        println!("No or invalid arguments passed in...running defaults...");
    }
    
    return settings;
}