use winreg::enums::*;
use winreg::RegKey;
use crate::PlatformInfo;

pub struct WindowsPlatformInfo { }

impl PlatformInfo for WindowsPlatformInfo {
    fn get_cpu_name() -> String {
        let hkey_local_machine = RegKey::predef(HKEY_LOCAL_MACHINE);
        let cur_ver = hkey_local_machine.open_subkey(r"HARDWARE\DESCRIPTION\System\CentralProcessor\0").unwrap();
        return cur_ver.get_value("ProcessorNameString").unwrap();
    }
}