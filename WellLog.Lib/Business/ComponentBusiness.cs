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
        public IEnumerable<Component> ReadFileHeaderLogicalRecord(Stream dlisStream)
        {
            if (dlisStream == null || dlisStream.IsAtEndOfStream()) { return null; }

            return new List<Component>
            {
                new AttributeComponent<string>
                {
                    Descriptor = new ComponentDescriptor(dlisStream.ReadUSHORT()),
                    Label = dlisStream.ReadASCII(),
                    RepresentationCode = dlisStream.ReadUSHORT()
                },

                new AttributeComponent<string>
                {
                    Descriptor = new ComponentDescriptor(dlisStream.ReadUSHORT()),
                    Label = dlisStream.ReadASCII(),
                    RepresentationCode = dlisStream.ReadUSHORT()
                },

                new ObjectComponent
                {
                    Descriptor = new ComponentDescriptor(dlisStream.ReadUSHORT()),
                    Name = dlisStream.ReadOBNAME()
                },

                new AttributeComponent<string>
                {
                    Descriptor = new ComponentDescriptor(dlisStream.ReadUSHORT()),
                    Count = 1,
                    Value = new string[1] { dlisStream.ReadASCII() }
                },

                new AttributeComponent<string>
                {
                    Descriptor = new ComponentDescriptor(dlisStream.ReadUSHORT()),
                    Count = 1,
                    Value = new string[1] { dlisStream.ReadASCII() }
                }
            };
        }

        public Component ReadComponent(Stream dlisStream)
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
                string label = descriptor.DoesAttributeHaveLabel ? dlisStream.ReadIDENT() : null;
                uint count = descriptor.DoesAttributeHaveCount ? dlisStream.ReadUVARI() : 1;
                byte representationCode = dlisStream.ReadUSHORT();
                string units = descriptor.DoesAttributeHaveUnits ? dlisStream.ReadUNITS() : null;

                switch ((RepresentationCode)representationCode)
                {
                    case RepresentationCode.FSHORT:
                        return new AttributeComponent<float>
                        {
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadFSHORT(count)
                        };
                    case RepresentationCode.FSINGL:
                        return new AttributeComponent<float>
                        {
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadFSINGL(count)
                        };
                    case RepresentationCode.FSING1:
                        return new AttributeComponent<FSING1>
                        {
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadFSING1(count)
                        };
                    case RepresentationCode.FSING2:
                        return new AttributeComponent<FSING2>
                        {
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadFSING2(count)
                        };
                    case RepresentationCode.ISINGL:
                        return new AttributeComponent<float>
                        {
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadISINGL(count)
                        };
                    case RepresentationCode.VSINGL:
                        return new AttributeComponent<float>
                        {
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadVSINGL(count)
                        };
                    case RepresentationCode.FDOUBL:
                        return new AttributeComponent<double>
                        {
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadFDOUBL(count)
                        };
                    case RepresentationCode.FDOUB1:
                        return new AttributeComponent<FDOUB1>
                        {
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadFDOUB1(count)
                        };
                    case RepresentationCode.FDOUB2:
                        return new AttributeComponent<FDOUB2>
                        {
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadFDOUB2(count)
                        };
                    case RepresentationCode.CSINGL:
                        return new AttributeComponent<CSINGL>
                        {
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadCSINGL(count)
                        };
                    case RepresentationCode.CDOUBL:
                        return new AttributeComponent<CDOUBL>
                        {
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadCDOUBL(count)
                        };
                    case RepresentationCode.SSHORT:
                        return new AttributeComponent<sbyte>
                        {
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadSSHORT(count)
                        };
                    case RepresentationCode.SNORM:
                        return new AttributeComponent<short>
                        {
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadSNORM(count)
                        };
                    case RepresentationCode.SLONG:
                        return new AttributeComponent<int>
                        {
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadSLONG(count)
                        };
                    case RepresentationCode.USHORT:
                        return new AttributeComponent<byte>
                        {
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadUSHORT(count)
                        };
                    case RepresentationCode.UNORM:
                        return new AttributeComponent<ushort>
                        {
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadUNORM(count)
                        };
                    case RepresentationCode.ULONG:
                        return new AttributeComponent<uint>
                        {
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadULONG(count)
                        };
                    case RepresentationCode.UVARI:
                        return new AttributeComponent<uint>
                        {
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadUVARI(count)
                        };
                    case RepresentationCode.IDENT:
                        return new AttributeComponent<string>
                        {
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadIDENT(count)
                        };
                    case RepresentationCode.ASCII:
                        return new AttributeComponent<string>
                        {
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadASCII(count)
                        };
                    case RepresentationCode.DTIME:
                        return new AttributeComponent<DateTime>
                        {
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadDTIME(count)
                        };
                    case RepresentationCode.ORIGIN:
                        return new AttributeComponent<uint>
                        {
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadUVARI(count)
                        };
                    case RepresentationCode.OBNAME:
                        return new AttributeComponent<OBNAME>
                        {
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadOBNAME(count)
                        };
                    case RepresentationCode.OBJREF:
                        return new AttributeComponent<OBJREF>
                        {
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadOBJREF(count)
                        };
                    case RepresentationCode.ATTREF:
                        return new AttributeComponent<ATTREF>
                        {
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadATTREF(count)
                        };
                    case RepresentationCode.STATUS:
                        return new AttributeComponent<bool>
                        {
                            Label = label,
                            Count = count,
                            RepresentationCode = representationCode,
                            Units = units,
                            Value = dlisStream.ReadSTATUS(count)
                        };
                    case RepresentationCode.UNITS:
                        return new AttributeComponent<string>
                        {
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
