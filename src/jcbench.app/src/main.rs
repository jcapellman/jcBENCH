use chrono;
use md5;


fn bench_md5() -> i32 {
    let seconds_to_run:i64 = 20;

    let start_time = chrono::offset::Local::now();

    let mut number_iterations:i32 = 0;

    while (chrono::offset::Local::now() - start_time).num_seconds() < seconds_to_run
    {
        let result:md5::Digest = md5::compute(b"abcdefghijklmnopqrstuvwxyz");
        
        if (chrono::offset::Local::now() - start_time).num_seconds() > seconds_to_run
        {
            break;
        }

        number_iterations = number_iterations + 1;
    }

    return number_iterations;
}

fn main() {
    let result:i32 = bench_md5();

    println!("Hashing Benchmark Score: {result}");
}
