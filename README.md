
## What is GRemote?

GRemote is a tool for recording and viewing a gaming
session over a network. Some of the problems and challenges
with doing this are:

* Broadcasting your screen usually involves a public
  service like Twitch or a whole-stack RTP server.

* Latency with most broadcasting servers is not designed
  for real-time applications.

* Many applications use DirectInput and other APIs that
  make capturing or replaying input difficult or not
  compatible with many tools.

## Download

Not creating releases until `0.1.0`

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
if you wish.

This project **does not** modify, redistribute, or even
link against FFMpeg, which uses the GPL/LGPL.
