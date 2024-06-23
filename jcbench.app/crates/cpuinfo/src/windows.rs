use winreg::enums::*;
use winreg::RegKey;
use crate::PlatformInfo;

pub struct WindowsPlatformInfo { }

impl PlatformInfo for WindowsPlatformInfo {
    fn get_cpu_name() -> String {
        let hive_key_local_machine = RegKey::predef(HKEY_LOCAL_MACHINE);

        return match hive_key_local_machine.open_subkey(r"HARDWARE\DESCRIPTION\System\CentralProcessor\0") {
            Err(_e) => return "(Unknown CPU)".to_string(),
            Ok(registry_key) => registry_key.get_value("ProcessorNameString").unwrap()
        };
    }
}