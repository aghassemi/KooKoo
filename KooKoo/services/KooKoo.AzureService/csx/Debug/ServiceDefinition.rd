<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="KooKoo.AzureService" generation="1" functional="0" release="0" Id="13f8f361-9a58-46ad-90f5-ef13e8695186" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="KooKoo.AzureServiceGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="KooKoo.WebService:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/KooKoo.AzureService/KooKoo.AzureServiceGroup/LB:KooKoo.WebService:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="KooKoo.WebService:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/KooKoo.AzureService/KooKoo.AzureServiceGroup/MapKooKoo.WebService:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="KooKoo.WebService:StorageConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/KooKoo.AzureService/KooKoo.AzureServiceGroup/MapKooKoo.WebService:StorageConnectionString" />
          </maps>
        </aCS>
        <aCS name="KooKoo.WebServiceInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/KooKoo.AzureService/KooKoo.AzureServiceGroup/MapKooKoo.WebServiceInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:KooKoo.WebService:Endpoint1">
          <toPorts>
            <inPortMoniker name="/KooKoo.AzureService/KooKoo.AzureServiceGroup/KooKoo.WebService/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapKooKoo.WebService:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/KooKoo.AzureService/KooKoo.AzureServiceGroup/KooKoo.WebService/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapKooKoo.WebService:StorageConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/KooKoo.AzureService/KooKoo.AzureServiceGroup/KooKoo.WebService/StorageConnectionString" />
          </setting>
        </map>
        <map name="MapKooKoo.WebServiceInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/KooKoo.AzureService/KooKoo.AzureServiceGroup/KooKoo.WebServiceInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="KooKoo.WebService" generation="1" functional="0" release="0" software="C:\Users\Ali\Documents\GitHub\KooKoo\KooKoo\services\KooKoo.AzureService\csx\Debug\roles\KooKoo.WebService" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="1792" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="StorageConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;KooKoo.WebService&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;KooKoo.WebService&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/KooKoo.AzureService/KooKoo.AzureServiceGroup/KooKoo.WebServiceInstances" />
            <sCSPolicyUpdateDomainMoniker name="/KooKoo.AzureService/KooKoo.AzureServiceGroup/KooKoo.WebServiceUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/KooKoo.AzureService/KooKoo.AzureServiceGroup/KooKoo.WebServiceFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="KooKoo.WebServiceUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="KooKoo.WebServiceFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="KooKoo.WebServiceInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="a01948f6-70d7-4f42-9964-df6d04b546a1" ref="Microsoft.RedDog.Contract\ServiceContract\KooKoo.AzureServiceContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="601fe80e-6ffe-4505-b620-510045a384df" ref="Microsoft.RedDog.Contract\Interface\KooKoo.WebService:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/KooKoo.AzureService/KooKoo.AzureServiceGroup/KooKoo.WebService:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>