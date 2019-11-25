<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
  <xsl:output method="xml" indent="yes"/>

  <xsl:template match="@* | node()">
    <xsl:copy>
      <xsl:apply-templates select="@* | node()"/>
    </xsl:copy>
  </xsl:template>

  <xsl:template match="Database">
    <xsl:element name="Database">
      <xsl:attribute name="Type">
        <xsl:value-of select="@DatabaseName"/>
      </xsl:attribute>
      <xsl:if test="@ReleaseVersion">
        <xsl:attribute name="ReleaseVersion">
          <xsl:value-of select="@ReleaseVersion"/>
        </xsl:attribute>
      </xsl:if>
			<xsl:if test="@SupplementaryProductName">
				<xsl:attribute name="SupplementaryProductName">
					<xsl:value-of select="@SupplementaryProductName"/>
				</xsl:attribute>
			</xsl:if>
			<xsl:copy-of select="./*"/>
    </xsl:element>
  </xsl:template>
</xsl:stylesheet>

