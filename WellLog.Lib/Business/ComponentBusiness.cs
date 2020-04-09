using System;
using System.Collections.Generic;
using System.IO;
using WellLog.Lib.Enumerations.DLIS;
using WellLog.Lib.Helpers;
using WellLog.Lib.Models.DLIS;

namespace WellLog.Lib.Business
{
    public class ComponentBusiness : IComponentBusiness
    {
        public const string DEFAULT_LABEL = "";
        public const uint DEFAULT_COUNT = 1;
        public const byte DEFAULT_REP_CODE = 19;
        public const string DEFAULT_UNITS = "";

        public ComponentBase ReadComponent(Stream dlisStream, RepresentationCode valueRepCode)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return null; }

            var descriptor = new ComponentDescriptor(dlisStream.ReadUSHORT());
            if (descriptor.IsSet || descriptor.IsRedundantSet || descriptor.IsReplacementSet)
            {
                var setComponent = new SetComponent { Descriptor = descriptor };
                if (descriptor.DoesSetHaveType) { setComponent.Type = dlisStream.ReadIDENT(); }
                if (descriptor.DoesSetHaveName) { setComponent.Name = dlisStream.ReadIDENT(); }
                return setComponent;
            }

            if (descriptor.IsObject)
            {
                var objComponent = new ObjectComponent { Descriptor = descriptor };
                if (descriptor.DoesObjectHaveName) { objComponent.Name = dlisStream.ReadOBNAME(); }
                return objComponent;
            }

            if (descriptor.IsAttribute || descriptor.IsInvariantAttribute)
            {
                string label = descriptor.DoesAttributeHaveLabel ? dlisStream.ReadIDENT() : DEFAULT_LABEL;
                uint count = descriptor.DoesAttributeHaveCount ? dlisStream.ReadUVARI() : DEFAULT_COUNT;
                byte representationCode = descriptor.DoesAttributeHaveRepresentationCode ? dlisStream.ReadUSHORT() : DEFAULT_REP_CODE;
                string units = descriptor.DoesAttributeHaveUnits ? dlisStream.ReadUNITS() : DEFAULT_UNITS;

                switch (valueRepCode)
                {
                    case RepresentationCode.FSHORT:
                        return new AttributeComponent<float>
                        {
                            Descriptor = descriptor,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = descriptor.DoesAttributeHaveValue ? dlisStream.ReadFSHORT(count) : null
                        };
                    case RepresentationCode.FSINGL:
                        return new AttributeComponent<float>
                        {
                            Descriptor = descriptor,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = descriptor.DoesAttributeHaveValue ? dlisStream.ReadFSINGL(count) : null
                        };
                    case RepresentationCode.FSING1:
                        return new AttributeComponent<FSING1>
                        {
                            Descriptor = descriptor,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = descriptor.DoesAttributeHaveValue ? dlisStream.ReadFSING1(count) : null
                        };
                    case RepresentationCode.FSING2:
                        return new AttributeComponent<FSING2>
                        {
                            Descriptor = descriptor,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = descriptor.DoesAttributeHaveValue ? dlisStream.ReadFSING2(count) : null
                        };
                    case RepresentationCode.ISINGL:
                        return new AttributeComponent<float>
                        {
                            Descriptor = descriptor,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = descriptor.DoesAttributeHaveValue ? dlisStream.ReadISINGL(count) : null
                        };
                    case RepresentationCode.VSINGL:
                        return new AttributeComponent<float>
                        {
                            Descriptor = descriptor,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = descriptor.DoesAttributeHaveValue ? dlisStream.ReadVSINGL(count) : null
                        };
                    case RepresentationCode.FDOUBL:
                        return new AttributeComponent<double>
                        {
                            Descriptor = descriptor,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = descriptor.DoesAttributeHaveValue ? dlisStream.ReadFDOUBL(count) : null
                        };
                    case RepresentationCode.FDOUB1:
                        return new AttributeComponent<FDOUB1>
                        {
                            Descriptor = descriptor,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = descriptor.DoesAttributeHaveValue ? dlisStream.ReadFDOUB1(count) : null
                        };
                    case RepresentationCode.FDOUB2:
                        return new AttributeComponent<FDOUB2>
                        {
                            Descriptor = descriptor,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = descriptor.DoesAttributeHaveValue ? dlisStream.ReadFDOUB2(count) : null
                        };
                    case RepresentationCode.CSINGL:
                        return new AttributeComponent<CSINGL>
                        {
                            Descriptor = descriptor,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = descriptor.DoesAttributeHaveValue ? dlisStream.ReadCSINGL(count) : null
                        };
                    case RepresentationCode.CDOUBL:
                        return new AttributeComponent<CDOUBL>
                        {
                            Descriptor = descriptor,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = descriptor.DoesAttributeHaveValue ? dlisStream.ReadCDOUBL(count) : null
                        };
                    case RepresentationCode.SSHORT:
                        return new AttributeComponent<sbyte>
                        {
                            Descriptor = descriptor,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = descriptor.DoesAttributeHaveValue ? dlisStream.ReadSSHORT(count) : null
                        };
                    case RepresentationCode.SNORM:
                        return new AttributeComponent<short>
                        {
                            Descriptor = descriptor,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = descriptor.DoesAttributeHaveValue ? dlisStream.ReadSNORM(count) : null
                        };
                    case RepresentationCode.SLONG:
                        return new AttributeComponent<int>
                        {
                            Descriptor = descriptor,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = descriptor.DoesAttributeHaveValue ? dlisStream.ReadSLONG(count) : null
                        };
                    case RepresentationCode.USHORT:
                        return new AttributeComponent<byte>
                        {
                            Descriptor = descriptor,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = descriptor.DoesAttributeHaveValue ? dlisStream.ReadUSHORT(count) : null
                        };
                    case RepresentationCode.UNORM:
                        return new AttributeComponent<ushort>
                        {
                            Descriptor = descriptor,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = descriptor.DoesAttributeHaveValue ? dlisStream.ReadUNORM(count) : null
                        };
                    case RepresentationCode.ULONG:
                        return new AttributeComponent<uint>
                        {
                            Descriptor = descriptor,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = descriptor.DoesAttributeHaveValue ? dlisStream.ReadULONG(count) : null
                        };
                    case RepresentationCode.UVARI:
                        return new AttributeComponent<uint>
                        {
                            Descriptor = descriptor,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = descriptor.DoesAttributeHaveValue ? dlisStream.ReadUVARI(count) : null
                        };
                    case RepresentationCode.IDENT:
                        return new AttributeComponent<string>
                        {
                            Descriptor = descriptor,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = descriptor.DoesAttributeHaveValue ? dlisStream.ReadIDENT(count) : null
                        };
                    case RepresentationCode.ASCII:
                        return new AttributeComponent<string>
                        {
                            Descriptor = descriptor,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = descriptor.DoesAttributeHaveValue ? dlisStream.ReadASCII(count) : null
                        };
                    case RepresentationCode.DTIME:
                        return new AttributeComponent<DateTime>
                        {
                            Descriptor = descriptor,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = descriptor.DoesAttributeHaveValue ? dlisStream.ReadDTIME(count) : null
                        };
                    case RepresentationCode.ORIGIN:
                        return new AttributeComponent<uint>
                        {
                            Descriptor = descriptor,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = descriptor.DoesAttributeHaveValue ? dlisStream.ReadUVARI(count) : null
                        };
                    case RepresentationCode.OBNAME:
                        return new AttributeComponent<OBNAME>
                        {
                            Descriptor = descriptor,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = descriptor.DoesAttributeHaveValue ? dlisStream.ReadOBNAME(count) : null
                        };
                    case RepresentationCode.OBJREF:
                        return new AttributeComponent<OBJREF>
                        {
                            Descriptor = descriptor,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = descriptor.DoesAttributeHaveValue ? dlisStream.ReadOBJREF(count) : null
                        };
                    case RepresentationCode.ATTREF:
                        return new AttributeComponent<ATTREF>
                        {
                            Descriptor = descriptor,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = descriptor.DoesAttributeHaveValue ? dlisStream.ReadATTREF(count) : null
                        };
                    case RepresentationCode.STATUS:
                        return new AttributeComponent<bool>
                        {
                            Descriptor = descriptor,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = descriptor.DoesAttributeHaveValue ? dlisStream.ReadSTATUS(count) : null
                        };
                    case RepresentationCode.UNITS:
                        return new AttributeComponent<string>
                        {
                            Descriptor = descriptor,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = descriptor.DoesAttributeHaveValue ? dlisStream.ReadUNITS(count) : null
                        };
                }
            }

            return null;
        }
    }
}
