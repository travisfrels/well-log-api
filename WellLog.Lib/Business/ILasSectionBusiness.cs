﻿using System.IO;
using WellLog.Lib.Models;

namespace WellLog.Lib.Business
{
    public interface ILasSectionBusiness
    {
        LasSection ReadSection(Stream lasStream);
        void WriteSection(Stream lasStream, LasSection lasSection);
    }
}