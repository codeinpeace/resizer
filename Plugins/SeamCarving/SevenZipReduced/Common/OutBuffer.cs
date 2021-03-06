// Copyright (c) Imazen LLC.
// No part of this project, including this file, may be copied, modified,
// propagated, or distributed except as permitted in COPYRIGHT.txt.
// Licensed under the GNU Affero General Public License, Version 3.0.
// Commercial licenses available at http://imageresizing.net/
// OutBuffer.cs
#pragma warning disable 1591
namespace SevenZipReduced.Buffer
{
    public class OutBuffer
    {
        byte[] m_Buffer;
        uint m_Pos;
        uint m_BufferSize;
        System.IO.Stream m_Stream;
        ulong m_ProcessedSize;

        public OutBuffer(uint bufferSize)
        {
            m_Buffer = new byte[bufferSize];
            m_BufferSize = bufferSize;
        }

        public void SetStream(System.IO.Stream stream) { m_Stream = stream; }
        public void FlushStream() { m_Stream.Flush(); }
        public void CloseStream() { m_Stream.Close(); }
        public void ReleaseStream() { m_Stream = null; }

        public void Init()
        {
            m_ProcessedSize = 0;
            m_Pos = 0;
        }

        public void WriteByte(byte b)
        {
            m_Buffer[m_Pos++] = b;
            if (m_Pos >= m_BufferSize)
                FlushData();
        }

        public void FlushData()
        {
            if (m_Pos == 0)
                return;
            m_Stream.Write(m_Buffer, 0, (int)m_Pos);
            m_Pos = 0;
        }

        public ulong GetProcessedSize() { return m_ProcessedSize + m_Pos; }
    }
}
