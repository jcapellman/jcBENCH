use std::fs::read_to_string;
use std::collections::HashMap;

use crate::PlatformInfo;

pub struct LinuxPlatformInfo { }

fn read_proc_cpuinfo() -> HashMap<String, String> {
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

fn get_key(info: HashMap<String, String>, key: &str) -> String {
    let key = info.get(key);

    if key == None {
        return "".to_string();
    }

    return key.unwrap().to_string();
}

impl PlatformInfo for LinuxPlatformInfo {
    fn get_cpu_name() -> String {
        let proc_cpuinfo = read_proc_cpuinfo();

        let mut cpu_name = get_key(proc_cpuinfo.clone(), "uarch");

        if cpu_name == "" {
            cpu_name = get_key(proc_cpuinfo.clone(), "model name");
        }

        if cpu_name == "" {
            return "Unknown".to_string();
        }

        return cpu_name;
    }
}