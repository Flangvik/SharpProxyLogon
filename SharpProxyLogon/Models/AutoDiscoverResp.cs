using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpProxyLogon.Models
{


    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/exchange/autodiscover/responseschema/2006")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.microsoft.com/exchange/autodiscover/responseschema/2006", IsNullable = false)]
    public partial class Autodiscover
    {

        private Response responseField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://schemas.microsoft.com/exchange/autodiscover/outlook/responseschema/2006a")]
        public Response Response
        {
            get
            {
                return this.responseField;
            }
            set
            {
                this.responseField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/exchange/autodiscover/outlook/responseschema/2006a")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.microsoft.com/exchange/autodiscover/outlook/responseschema/2006a", IsNullable = false)]
    public partial class Response
    {

        private ResponseUser userField;

        private ResponseAccount accountField;

        /// <remarks/>
        public ResponseUser User
        {
            get
            {
                return this.userField;
            }
            set
            {
                this.userField = value;
            }
        }

        /// <remarks/>
        public ResponseAccount Account
        {
            get
            {
                return this.accountField;
            }
            set
            {
                this.accountField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/exchange/autodiscover/outlook/responseschema/2006a")]
    public partial class ResponseUser
    {

        private string displayNameField;

        private string legacyDNField;

        private string autoDiscoverSMTPAddressField;

        private string deploymentIdField;

        /// <remarks/>
        public string DisplayName
        {
            get
            {
                return this.displayNameField;
            }
            set
            {
                this.displayNameField = value;
            }
        }

        /// <remarks/>
        public string LegacyDN
        {
            get
            {
                return this.legacyDNField;
            }
            set
            {
                this.legacyDNField = value;
            }
        }

        /// <remarks/>
        public string AutoDiscoverSMTPAddress
        {
            get
            {
                return this.autoDiscoverSMTPAddressField;
            }
            set
            {
                this.autoDiscoverSMTPAddressField = value;
            }
        }

        /// <remarks/>
        public string DeploymentId
        {
            get
            {
                return this.deploymentIdField;
            }
            set
            {
                this.deploymentIdField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/exchange/autodiscover/outlook/responseschema/2006a")]
    public partial class ResponseAccount
    {

        private string accountTypeField;

        private string actionField;

        private string microsoftOnlineField;

        private ResponseAccountProtocol[] protocolField;

        /// <remarks/>
        public string AccountType
        {
            get
            {
                return this.accountTypeField;
            }
            set
            {
                this.accountTypeField = value;
            }
        }

        /// <remarks/>
        public string Action
        {
            get
            {
                return this.actionField;
            }
            set
            {
                this.actionField = value;
            }
        }

        /// <remarks/>
        public string MicrosoftOnline
        {
            get
            {
                return this.microsoftOnlineField;
            }
            set
            {
                this.microsoftOnlineField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Protocol")]
        public ResponseAccountProtocol[] Protocol
        {
            get
            {
                return this.protocolField;
            }
            set
            {
                this.protocolField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/exchange/autodiscover/outlook/responseschema/2006a")]
    public partial class ResponseAccountProtocol
    {

        private string typeField;

        private ResponseAccountProtocolInternal internalField;

        private string serverField;

        private string sSLField;

        private string authPackageField;

        private string serverDNField;

        private string serverVersionField;

        private string mdbDNField;

        private string publicFolderServerField;

        private string adField;

        private string aSUrlField;

        private string ewsUrlField;

        private string emwsUrlField;

        private string ecpUrlField;

        private string ecpUrlumField;

        private string ecpUrlaggrField;

        private string ecpUrlmtField;

        private string ecpUrlretField;

        private string ecpUrlsmsField;

        private string ecpUrlphotoField;

        private string ecpUrltmField;

        private string ecpUrltmCreatingField;

        private string ecpUrltmEditingField;

        private string ecpUrlextinstallField;

        private string oOFUrlField;

        private string uMUrlField;

        private string oABUrlField;

        private string serverExclusiveConnectField;

        private string certPrincipalNameField;

        private string groupingInformationField;

        /// <remarks/>
        public string Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        public ResponseAccountProtocolInternal Internal
        {
            get
            {
                return this.internalField;
            }
            set
            {
                this.internalField = value;
            }
        }

        /// <remarks/>
        public string Server
        {
            get
            {
                return this.serverField;
            }
            set
            {
                this.serverField = value;
            }
        }

        /// <remarks/>
        public string SSL
        {
            get
            {
                return this.sSLField;
            }
            set
            {
                this.sSLField = value;
            }
        }

        /// <remarks/>
        public string AuthPackage
        {
            get
            {
                return this.authPackageField;
            }
            set
            {
                this.authPackageField = value;
            }
        }

        /// <remarks/>
        public string ServerDN
        {
            get
            {
                return this.serverDNField;
            }
            set
            {
                this.serverDNField = value;
            }
        }

        /// <remarks/>
        public string ServerVersion
        {
            get
            {
                return this.serverVersionField;
            }
            set
            {
                this.serverVersionField = value;
            }
        }

        /// <remarks/>
        public string MdbDN
        {
            get
            {
                return this.mdbDNField;
            }
            set
            {
                this.mdbDNField = value;
            }
        }

        /// <remarks/>
        public string PublicFolderServer
        {
            get
            {
                return this.publicFolderServerField;
            }
            set
            {
                this.publicFolderServerField = value;
            }
        }

        /// <remarks/>
        public string AD
        {
            get
            {
                return this.adField;
            }
            set
            {
                this.adField = value;
            }
        }

        /// <remarks/>
        public string ASUrl
        {
            get
            {
                return this.aSUrlField;
            }
            set
            {
                this.aSUrlField = value;
            }
        }

        /// <remarks/>
        public string EwsUrl
        {
            get
            {
                return this.ewsUrlField;
            }
            set
            {
                this.ewsUrlField = value;
            }
        }

        /// <remarks/>
        public string EmwsUrl
        {
            get
            {
                return this.emwsUrlField;
            }
            set
            {
                this.emwsUrlField = value;
            }
        }

        /// <remarks/>
        public string EcpUrl
        {
            get
            {
                return this.ecpUrlField;
            }
            set
            {
                this.ecpUrlField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("EcpUrl-um")]
        public string EcpUrlum
        {
            get
            {
                return this.ecpUrlumField;
            }
            set
            {
                this.ecpUrlumField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("EcpUrl-aggr")]
        public string EcpUrlaggr
        {
            get
            {
                return this.ecpUrlaggrField;
            }
            set
            {
                this.ecpUrlaggrField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("EcpUrl-mt")]
        public string EcpUrlmt
        {
            get
            {
                return this.ecpUrlmtField;
            }
            set
            {
                this.ecpUrlmtField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("EcpUrl-ret")]
        public string EcpUrlret
        {
            get
            {
                return this.ecpUrlretField;
            }
            set
            {
                this.ecpUrlretField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("EcpUrl-sms")]
        public string EcpUrlsms
        {
            get
            {
                return this.ecpUrlsmsField;
            }
            set
            {
                this.ecpUrlsmsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("EcpUrl-photo")]
        public string EcpUrlphoto
        {
            get
            {
                return this.ecpUrlphotoField;
            }
            set
            {
                this.ecpUrlphotoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("EcpUrl-tm")]
        public string EcpUrltm
        {
            get
            {
                return this.ecpUrltmField;
            }
            set
            {
                this.ecpUrltmField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("EcpUrl-tmCreating")]
        public string EcpUrltmCreating
        {
            get
            {
                return this.ecpUrltmCreatingField;
            }
            set
            {
                this.ecpUrltmCreatingField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("EcpUrl-tmEditing")]
        public string EcpUrltmEditing
        {
            get
            {
                return this.ecpUrltmEditingField;
            }
            set
            {
                this.ecpUrltmEditingField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("EcpUrl-extinstall")]
        public string EcpUrlextinstall
        {
            get
            {
                return this.ecpUrlextinstallField;
            }
            set
            {
                this.ecpUrlextinstallField = value;
            }
        }

        /// <remarks/>
        public string OOFUrl
        {
            get
            {
                return this.oOFUrlField;
            }
            set
            {
                this.oOFUrlField = value;
            }
        }

        /// <remarks/>
        public string UMUrl
        {
            get
            {
                return this.uMUrlField;
            }
            set
            {
                this.uMUrlField = value;
            }
        }

        /// <remarks/>
        public string OABUrl
        {
            get
            {
                return this.oABUrlField;
            }
            set
            {
                this.oABUrlField = value;
            }
        }

        /// <remarks/>
        public string ServerExclusiveConnect
        {
            get
            {
                return this.serverExclusiveConnectField;
            }
            set
            {
                this.serverExclusiveConnectField = value;
            }
        }

        /// <remarks/>
        public string CertPrincipalName
        {
            get
            {
                return this.certPrincipalNameField;
            }
            set
            {
                this.certPrincipalNameField = value;
            }
        }

        /// <remarks/>
        public string GroupingInformation
        {
            get
            {
                return this.groupingInformationField;
            }
            set
            {
                this.groupingInformationField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/exchange/autodiscover/outlook/responseschema/2006a")]
    public partial class ResponseAccountProtocolInternal
    {

        private ResponseAccountProtocolInternalOWAUrl oWAUrlField;

        private ResponseAccountProtocolInternalProtocol protocolField;

        /// <remarks/>
        public ResponseAccountProtocolInternalOWAUrl OWAUrl
        {
            get
            {
                return this.oWAUrlField;
            }
            set
            {
                this.oWAUrlField = value;
            }
        }

        /// <remarks/>
        public ResponseAccountProtocolInternalProtocol Protocol
        {
            get
            {
                return this.protocolField;
            }
            set
            {
                this.protocolField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/exchange/autodiscover/outlook/responseschema/2006a")]
    public partial class ResponseAccountProtocolInternalOWAUrl
    {

        private string authenticationMethodField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string AuthenticationMethod
        {
            get
            {
                return this.authenticationMethodField;
            }
            set
            {
                this.authenticationMethodField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.microsoft.com/exchange/autodiscover/outlook/responseschema/2006a")]
    public partial class ResponseAccountProtocolInternalProtocol
    {

        private string typeField;

        private string aSUrlField;

        /// <remarks/>
        public string Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        public string ASUrl
        {
            get
            {
                return this.aSUrlField;
            }
            set
            {
                this.aSUrlField = value;
            }
        }
    }


  
}
