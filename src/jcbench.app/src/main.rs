use chrono;
use md5;
use sysinfo::System;

fn bench_md5() -> u32 {
    const SECONDS_TO_RUN:i64 = 2;

    let start_time = chrono::offset::Local::now();

    let mut number_iterations:u32 = 0;

    while (chrono::offset::Local::now() - start_time).num_seconds() < SECONDS_TO_RUN
    {
        let _result:md5::Digest = md5::compute(chrono::offset::Local::now().to_string());
        
        if (chrono::offset::Local::now() - start_time).num_seconds() > SECONDS_TO_RUN
        {
            break;
        }

        number_iterations = number_iterations + 1;
    }

    return number_iterations;
}

fn write_centered(str: String) {
    println!("{}", str);
}

fn submit_result(benchmark: u32) -> bool {
    // serialize and submit to server
    return true;
}

fn main() {
    let result:u32 = bench_md5();

    // Console.BackgroundColor = ConsoleColor.Black;
    
    // Console.Clear();

    write_centered("jcBENCH 2024.5.0 (RUST Edition)".to_string());
    write_centered("(C) 2012-2024 Jarred Capellman".to_string());
    write_centered("Source code is available on https://github.com/jcapellman/jcBENCH".to_string());

    //Console.BackgroundColor = ConsoleColor.Black;

    let mut sys = System::new_all();
    
    sys.refresh_all();

    println!("Operating System:             {:?}", System::name());
    
    println!("---------------");
    println!("CPU Information");
    println!("---------------");
    println!("Manufacturer: {}", sys.global_cpu_info().brand());
    println!("Model: {}", sys.global_cpu_info().name());
    println!("Count: {}x{}", sys.cpus().len(), sys.global_cpu_info().frequency());
    println!("Architecture: {}", sys.global_cpu_info().brand());
    println!("---------------");

    println!("Hashing Benchmark Score: {result}");

    println!("Do you want to submit your result (y/n)?");

    let mut key = String::new();

    let _ = std::io::stdin().read_line(&mut key);

    if key.trim_end() != "y" {
        return;
    }

    let submission_result = submit_result(result);

    if submission_result {
        println!("Submission was successful");
    } else {
        println!("Submission failed");
    }
}
