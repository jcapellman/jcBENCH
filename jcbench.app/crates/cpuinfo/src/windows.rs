use sysinfo::System;

use crate::PlatformInfo;

pub struct WindowsPlatformInfo { }

impl PlatformInfo for WindowsPlatformInfo {
    fn get_cpu_name() -> String {
        let mut sys = System::new_all();
    
        sys.refresh_all();

        return sys.cpus()[0].brand().to_string();
    }
}