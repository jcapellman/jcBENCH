use std::fs::read_to_string;
use std::collections::HashMap;

use crate::PlatformInfo;

const CPU_INFO_KEYS: [&str; 2] = ["uarch", "model name"];

pub struct LinuxPlatformInfo { }

fn read_proc_cpu_info() -> HashMap<String, String> {
    let mut dict = HashMap::new();

    for line in read_to_string("/proc/cpuinfo").unwrap().lines() {
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