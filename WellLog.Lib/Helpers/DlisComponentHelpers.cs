using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Helpers
{
    public static class DlisComponentHelpers
    {
        public static Component ReadComponent(this Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return null; }

            var descriptor = new ComponentDescriptor(dlisStream.ReadUSHORT());
            if (descriptor.IsSet || descriptor.IsRedundantSet || descriptor.IsReplacementSet)
            {
                return new SetComponent
                {
                    Descriptor = descriptor,
                    Type = dlisStream.ReadIDENT(),
                    Name = dlisStream.ReadIDENT()
                };
            }

            if (descriptor.IsObject)
            {
                return new ObjectComponent
                {
                    Descriptor = descriptor,
                    Name = dlisStream.ReadOBNAME()
                };
            }

            if (descriptor.IsAttribute || descriptor.IsInvariantAttribute)
            {
                return new AttributeComponent
                {
                    Descriptor = descriptor,
                    Label = dlisStream.ReadIDENT(),
                    Count = dlisStream.ReadUVARI(),
                    RepresentationCode = dlisStream.ReadUSHORT(),
                    Units = dlisStream.ReadUNITS()
                };
            }

            return null;
        }
    }
}
