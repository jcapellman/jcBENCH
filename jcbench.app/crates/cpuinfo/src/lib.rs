use std::fs::read_to_string;
use std::collections::HashMap;
use sysinfo::System;
use std::env;

pub struct CurrentCPUInfo {
    pub model_name: String,
    pub num_cores: usize,
    pub architecture: String
}

pub struct CPUInfo {
}

impl CPUInfo {
    #[cfg(target_os = "linux")]
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

    #[cfg(target_os = "linux")]
    fn get_key(info: HashMap<String, String>, key: &str) -> String {
        return info.get(key).unwrap().to_string();
    }

    pub fn get_cpu_info() -> CurrentCPUInfo {
        let mut sys = System::new_all();
    
        sys.refresh_all();

        let mut cpu_info = CurrentCPUInfo {
            model_name: sys.cpus()[0].brand().to_string(),
            num_cores: sys.cpus().len(),
            architecture: env::consts::ARCH.to_string()
        };
        
        #[cfg(target_os = "linux")]
        if parsed_cpu_name.len() == 0 {
            let proc_cpuinfo = read_proc_cpuinfo();

            parsed_cpu_name = get_key(proc_cpuinfo, "uarch");
        }

        return cpu_info;
    }
}