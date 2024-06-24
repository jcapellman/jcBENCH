pub mod linux;
pub mod windows;

#[cfg(target_os="windows")]
use crate::windows::WindowsPlatformInfo;

#[cfg(target_os="linux")]
use crate::linux::LinuxPlatformInfo;

use sysinfo::System;
use std::env;

pub const FALL_BACK_CPU_NAME: &str = "(Unknown CPU)";

pub struct CurrentCPUInfo {
    pub model_name: String,
    pub num_cores: usize,
    pub architecture: String
}

pub struct CPUInfo {
}

pub trait PlatformInfo {
    fn get_cpu_name() -> String;
}

impl CPUInfo {
    pub fn get_cpu_info() -> CurrentCPUInfo {
        let mut sys = System::new_all();
    
        sys.refresh_all();

        let mut cpu_info = CurrentCPUInfo {
            model_name: FALL_BACK_CPU_NAME.to_string(),
            num_cores: sys.cpus().len(),
            architecture: env::consts::ARCH.to_string()
        };
        
        #[cfg(target_os="linux")] {
            cpu_info.model_name = LinuxPlatformInfo::get_cpu_name();
        }
        
        #[cfg(target_os="windows")] {
            cpu_info.model_name = WindowsPlatformInfo::get_cpu_name();
        } 
        
        return cpu_info;
    }
}