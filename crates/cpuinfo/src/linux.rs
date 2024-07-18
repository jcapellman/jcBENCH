use std::fs::read_to_string;
use std::collections::HashMap;
use std::path::Path;

use crate::PlatformInfo;

const CPU_INFO_KEYS: [&str; 2] = ["uarch", "model name"];

const FILE_NAME_CPU_INFO: &str = "/proc/cpuinfo";

pub struct LinuxPlatformInfo { }

fn convert_file_to_hash_map(file_string: String) -> HashMap<String, String> {
    let mut dict = HashMap::new();

    for line in file_string.lines() {
        let line_split: Vec<&str> = line.split(':').collect();

        if line_split.len() == 1 {
            continue;
        }

        let key = line_split[0].trim();
        let value = line_split[1].trim();

        dict.entry(key.to_string()).or_insert(value.to_string());
    }

    return dict;
}

fn read_proc_cpu_info() -> HashMap<String, String> {
    if !Path::new(FILE_NAME_CPU_INFO).exists() {
        panic!("File could not find {FILE_NAME_CPU_INFO} to read CPU INFO");
    }

    let file_string_result = read_to_string(FILE_NAME_CPU_INFO);

    return match file_string_result {
        Ok(file_string) => convert_file_to_hash_map(file_string),
        Err(error) => panic!("Could not read CPU INFO ({error:?}")
    };
}

fn get_key(info: HashMap<String, String>, keys: &[&str]) -> Option<String> {
    for &key in keys {
        let key = info.get(key);

        if key != None {
            return Some(key.unwrap().to_string());
        }
    }

    return None;
}

impl PlatformInfo for LinuxPlatformInfo {
    fn get_cpu_name() -> String {
        return get_key(read_proc_cpu_info(), &CPU_INFO_KEYS).unwrap_or_else(|| crate::FALL_BACK_CPU_NAME.to_string());
    }
}