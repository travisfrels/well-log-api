using System;
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

        public ComponentBase ReadComponent(Stream dlisStream, AttributeComponent template = null)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return null; }

            var startPosition = dlisStream.Position;
            var descriptor = new ComponentDescriptor(dlisStream.ReadUSHORT());
            if (descriptor.IsSet || descriptor.IsRedundantSet || descriptor.IsReplacementSet)
            {
                var setComponent = new SetComponent { Descriptor = descriptor, StartPosition = startPosition };
                if (descriptor.DoesSetHaveType) { setComponent.Type = dlisStream.ReadIDENT(); }
                if (descriptor.DoesSetHaveName) { setComponent.Name = dlisStream.ReadIDENT(); }
                return setComponent;
            }

            if (descriptor.IsObject)
            {
                var objComponent = new ObjectComponent { Descriptor = descriptor, StartPosition = startPosition };
                if (descriptor.DoesObjectHaveName) { objComponent.Name = dlisStream.ReadOBNAME(); }
                return objComponent;
            }

            if (descriptor.IsAttribute || descriptor.IsInvariantAttribute)
            {
                string label = descriptor.DoesAttributeHaveLabel ? dlisStream.ReadIDENT() : (template == null ? DEFAULT_LABEL : template.Label);
                uint count = descriptor.DoesAttributeHaveCount ? dlisStream.ReadUVARI() : (template == null ? DEFAULT_COUNT : template.Count);
                byte representationCode = descriptor.DoesAttributeHaveRepresentationCode ? dlisStream.ReadUSHORT() : (template == null ? DEFAULT_REP_CODE : template.RepresentationCode);
                string units = descriptor.DoesAttributeHaveUnits ? dlisStream.ReadUNITS() : (template == null ? DEFAULT_UNITS : template.Units);

                if (template == null || !descriptor.DoesAttributeHaveValue)
                {
                    return new AttributeComponent
                    {
                        Descriptor = descriptor,
                        StartPosition = startPosition,
                        Label = label,
                        Count = count,
                        RepresentationCode = representationCode,
                        Units = units
                    };
                }

                switch ((RepresentationCode)representationCode)
                {
                    case RepresentationCode.FSHORT:
                        return new ValueAttributeComponent<float>
                        {
                            Descriptor = descriptor,
                            StartPosition = startPosition,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadFSHORT(count)
                        };
                    case RepresentationCode.FSINGL:
                        return new ValueAttributeComponent<float>
                        {
                            Descriptor = descriptor,
                            StartPosition = startPosition,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadFSINGL(count)
                        };
                    case RepresentationCode.FSING1:
                        return new ValueAttributeComponent<FSING1>
                        {
                            Descriptor = descriptor,
                            StartPosition = startPosition,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadFSING1(count)
                        };
                    case RepresentationCode.FSING2:
                        return new ValueAttributeComponent<FSING2>
                        {
                            Descriptor = descriptor,
                            StartPosition = startPosition,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadFSING2(count)
                        };
                    case RepresentationCode.ISINGL:
                        return new ValueAttributeComponent<float>
                        {
                            Descriptor = descriptor,
                            StartPosition = startPosition,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadISINGL(count)
                        };
                    case RepresentationCode.VSINGL:
                        return new ValueAttributeComponent<float>
                        {
                            Descriptor = descriptor,
                            StartPosition = startPosition,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadVSINGL(count)
                        };
                    case RepresentationCode.FDOUBL:
                        return new ValueAttributeComponent<double>
                        {
                            Descriptor = descriptor,
                            StartPosition = startPosition,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadFDOUBL(count)
                        };
                    case RepresentationCode.FDOUB1:
                        return new ValueAttributeComponent<FDOUB1>
                        {
                            Descriptor = descriptor,
                            StartPosition = startPosition,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadFDOUB1(count)
                        };
                    case RepresentationCode.FDOUB2:
                        return new ValueAttributeComponent<FDOUB2>
                        {
                            Descriptor = descriptor,
                            StartPosition = startPosition,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadFDOUB2(count)
                        };
                    case RepresentationCode.CSINGL:
                        return new ValueAttributeComponent<CSINGL>
                        {
                            Descriptor = descriptor,
                            StartPosition = startPosition,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadCSINGL(count)
                        };
                    case RepresentationCode.CDOUBL:
                        return new ValueAttributeComponent<CDOUBL>
                        {
                            Descriptor = descriptor,
                            StartPosition = startPosition,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadCDOUBL(count)
                        };
                    case RepresentationCode.SSHORT:
                        return new ValueAttributeComponent<sbyte>
                        {
                            Descriptor = descriptor,
                            StartPosition = startPosition,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadSSHORT(count)
                        };
                    case RepresentationCode.SNORM:
                        return new ValueAttributeComponent<short>
                        {
                            Descriptor = descriptor,
                            StartPosition = startPosition,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadSNORM(count)
                        };
                    case RepresentationCode.SLONG:
                        return new ValueAttributeComponent<int>
                        {
                            Descriptor = descriptor,
                            StartPosition = startPosition,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadSLONG(count)
                        };
                    case RepresentationCode.USHORT:
                        return new ValueAttributeComponent<byte>
                        {
                            Descriptor = descriptor,
                            StartPosition = startPosition,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadUSHORT(count)
                        };
                    case RepresentationCode.UNORM:
                        return new ValueAttributeComponent<ushort>
                        {
                            Descriptor = descriptor,
                            StartPosition = startPosition,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadUNORM(count)
                        };
                    case RepresentationCode.ULONG:
                        return new ValueAttributeComponent<uint>
                        {
                            Descriptor = descriptor,
                            StartPosition = startPosition,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadULONG(count)
                        };
                    case RepresentationCode.UVARI:
                        return new ValueAttributeComponent<uint>
                        {
                            Descriptor = descriptor,
                            StartPosition = startPosition,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadUVARI(count)
                        };
                    case RepresentationCode.IDENT:
                        return new ValueAttributeComponent<string>
                        {
                            Descriptor = descriptor,
                            StartPosition = startPosition,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadIDENT(count)
                        };
                    case RepresentationCode.ASCII:
                        return new ValueAttributeComponent<string>
                        {
                            Descriptor = descriptor,
                            StartPosition = startPosition,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadASCII(count)
                        };
                    case RepresentationCode.DTIME:
                        return new ValueAttributeComponent<DateTime>
                        {
                            Descriptor = descriptor,
                            StartPosition = startPosition,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadDTIME(count)
                        };
                    case RepresentationCode.ORIGIN:
                        return new ValueAttributeComponent<uint>
                        {
                            Descriptor = descriptor,
                            StartPosition = startPosition,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadUVARI(count)
                        };
                    case RepresentationCode.OBNAME:
                        return new ValueAttributeComponent<OBNAME>
                        {
                            Descriptor = descriptor,
                            StartPosition = startPosition,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadOBNAME(count)
                        };
                    case RepresentationCode.OBJREF:
                        return new ValueAttributeComponent<OBJREF>
                        {
                            Descriptor = descriptor,
                            StartPosition = startPosition,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadOBJREF(count)
                        };
                    case RepresentationCode.ATTREF:
                        return new ValueAttributeComponent<ATTREF>
                        {
                            Descriptor = descriptor,
                            StartPosition = startPosition,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadATTREF(count)
                        };
                    case RepresentationCode.STATUS:
                        return new ValueAttributeComponent<bool>
                        {
                            Descriptor = descriptor,
                            StartPosition = startPosition,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadSTATUS(count)
                        };
                    case RepresentationCode.UNITS:
                        return new ValueAttributeComponent<string>
                        {
                            Descriptor = descriptor,
                            StartPosition = startPosition,
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadUNITS(count)
                        };
                }
            }

            return null;
        }
    }
}
