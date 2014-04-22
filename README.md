## What is GRemote?

GRemote is a tool for recording and viewing a gaming
session over a network. Some of the problems and challenges
with doing this are:

* Broadcasting your screen usually involves a public
  service like Twitch or a whole-stack RTP server. GRemote
  instead simply listens on ports. As long as one user has
  no firewall nobody needs to open a port.

* Latency with most broadcasting servers is not designed
  for real-time applications. Most services give you anywhere
  between 10-30 second delays. GRemote tries to stream as real-time
  as possible. Internally it uses FFMpeg with zero-latency settings
  and MPEG transport stream.

* Many applications use DirectInput and other APIs that
  make capturing or replaying input difficult or not
  compatible. GRemote uses an existing virutal keyboard
  driver to actually look exactly like user input. This is
  also capable of interacting with UAC dialogs.

## Download

Download the latest Windows release [here](https://github.com/krisives/GRemote/releases)

These releases include everything you need to run the program:

* GRemote build from Visual C# Express 2010 (`remote.exe`)
* FFMpeg static build from [Zeranoe](http://ffmpeg.zeranoe.com/builds/) (`ffmpeg.exe`)
* Interceptor library made by Jason Pang (`interceptor.dll`)
* Interception keyboard driver made by Francisco Lopes (`interception.dll` and `install-interception.dll`)
* Wget for checking for udpates, etc. (`wget.exe`, `libssl32.dll`, `libintl3.dll`, `libiconv2.dll`, and `libeay32.dll`)

## Build

Download the project from Github and open the `GRemote.sln`
file in Visual C# 2010 Express. You will need to gather
some other dependencies to build it:

* Grab a static version of `ffmpeg.exe` and place it
  at the root directory of the solution. It should be
  in the same directory as this `README.md` file.

* TODO Virtual keyboard driver instructions

## Work in Progress

Things that don't work yet:

* Audio recording will require a virtual device as
  loopback recording is not support on many devices.

* Recovery from network loss needs to be improved

## File a Bug

Please go to the
[Github Issues](https://github.com/krisives/GRemote/issues)
to file a bug. Don't forget to mention:

* What version of GRemote are you using?

* What version of Windows are you using?

* Are you running 32-bit or 64-bit windows? (It says in
  the My Computer properties in some versions)

## License

Released under a MIT license. See 
[LICENSE.md](https://github.com/krisives/GRemote/blob/master/LICENSE.md)
if you wish. Other projects used and their licenses:

* This project **does not** modify, or even link against FFMpeg,
  which uses the GPL/LGPL.

* This project makes use of the Interceptor virtual keyboard/mouse
  library created by Jason Pang. He never made it clear what license
  it was released under so I have contacted him.

* This project also makes use of the Interception C# bindings created
  by Francisco Lopes. He never made it clear what license it was
  released under so I have contacted him.


