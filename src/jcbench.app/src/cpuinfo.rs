use std::fs::read_to_string;
use std::collections::HashMap;

pub struct CPUInfo {
    info: HashMap<String, String>
}

impl CPUInfo {
    pub fn new() -> CPUInfo {
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

        return CPUInfo { info: dict};
    }

    pub fn get_key(&self, key: &str) -> String {
        return self.info.get(key).unwrap().to_string();
    }
}