use winreg::enums::*;
use winreg::RegKey;
use crate::{FALL_BACK_CPU_NAME, PlatformInfo};

pub struct WindowsPlatformInfo { }

impl PlatformInfo for WindowsPlatformInfo {
    fn get_cpu_name() -> String {
        let hive_key_local_machine = RegKey::predef(HKEY_LOCAL_MACHINE);

        return match hive_key_local_machine.open_subkey(r"HARDWARE\DESCRIPTION\System\CentralProcessor\0") {
            Err(_e) => return FALL_BACK_CPU_NAME.to_string(),
            Ok(registry_key) => registry_key.get_value("ProcessorNameString").unwrap_or(FALL_BACK_CPU_NAME.to_string())
        };
    }
}