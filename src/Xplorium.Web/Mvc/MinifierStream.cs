﻿namespace Xplorium.Web.Mvc {
    using System.IO;
    using System.Text;

    using Microsoft.Ajax.Utilities;

    public class MinifierStream : Stream {
        private readonly Stream _stream;
        public OptimizeMode OptimizeMode { get; set; }

        public MinifierStream(Stream stream, OptimizeMode optimizeMode) {
            _stream = stream;
            OptimizeMode = optimizeMode;
        }

        public override bool CanRead {
            get { return _stream.CanRead; }
        }

        public override bool CanSeek {
            get { return _stream.CanSeek; }
        }

        public override bool CanWrite {
            get { return _stream.CanWrite; }
        }

        public override void Flush() {
            _stream.Flush();
        }

        public override long Length {
            get { return _stream.Length; }
        }

        public override long Position {
            get { return _stream.Position; }
            set { _stream.Position = value; }
        }

        public override int Read(byte[] buffer, int offset, int count) {
            return _stream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin) {
            return _stream.Seek(offset, origin);
        }

        public override void SetLength(long value) {
            _stream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count) {
            if (OptimizeMode != OptimizeMode.None) {
                var input = Encoding.UTF8.GetString(buffer);
                if (OptimizeMode == OptimizeMode.Html)
                    input = new HtmlMinifier().Minify(input);
                else if (OptimizeMode == OptimizeMode.Css)
                    input = new Minifier().MinifyStyleSheet(input);
                //else if (OptimizeMode == OptimizeMode.Js)
                //  input = new Minifier().MinifyJavaScript(input);

                var output = Encoding.UTF8.GetBytes(input);
                _stream.Write(output, 0, output.GetLength(0));
            } else
                _stream.Write(buffer, offset, count);
        }
    }
}