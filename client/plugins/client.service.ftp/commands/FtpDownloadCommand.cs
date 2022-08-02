﻿using common.libs.extends;
using MessagePack;
using System;
using System.Text;

namespace client.service.ftp.commands
{
    public class FtpDownloadCommand : IFtpCommandBase
    {
        public FtpCommand Cmd { get; set; } = FtpCommand.DOWNLOAD;

        public string Path { get; set; }

        public string TargetPath { get; set; } = string.Empty;

        public byte[] ToBytes()
        {
            byte cmdByte = (byte)Cmd;

            byte[] path = Path.ToBytes();
            byte[] targetPath = TargetPath.ToBytes();
            byte[] pathLength = path.Length.ToBytes();
            byte[] targetPathLength = TargetPath.Length.ToBytes();

            var bytes = new byte[
                1 +
                path.Length + pathLength.Length
            ];
            bytes[0] = cmdByte;

            int index = 1;

            Array.Copy(pathLength, 0, bytes, index, pathLength.Length);
            index += 4;

            Array.Copy(targetPathLength, 0, bytes, index, targetPathLength.Length);
            index += 4;

            if (path.Length > 0)
            {
                Array.Copy(path, 0, bytes, index, path.Length);
                index += path.Length;
            }
            if (targetPath.Length > 0)
            {
                Array.Copy(targetPath, 0, bytes, index, targetPath.Length);
                index += targetPath.Length;
            }
            return bytes;
        }

        public void DeBytes(in ReadOnlyMemory<byte> bytes)
        {
            Cmd = (FtpCommand)bytes.Span[0];
            int index = 1;

            int pathLength = bytes.Span.Slice(index).ToInt32();
            index += 4;

            int targetPathLength = bytes.Span.Slice(index).ToInt32();
            index += 4;

            if (pathLength > 0)
            {
                Path = bytes.Span.Slice(index, pathLength).GetString();
            }
            if (targetPathLength > 0)
            {
                TargetPath = bytes.Span.Slice(index, targetPathLength).GetString();
            }
        }
    }
}
