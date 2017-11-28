// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NamespaceDoc.cs" company="imbVeles" >
//
// Copyright (C) 2017 imbVeles
//
// This program is free software: you can redistribute it and/or modify
// it under the +terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see http://www.gnu.org/licenses/. 
// </copyright>
// <summary>
// Project: imbACE.Core
// Author: Goran Grubic
// ------------------------------------------------------------------------------------------------------------------
// Project web site: http://blog.veles.rs
// GitHub: http://github.com/gorangrubic
// Mendeley profile: http://www.mendeley.com/profiles/goran-grubi2/
// ORCID ID: http://orcid.org/0000-0003-2673-9471
// Email: hardy@veles.rs
// </summary>
// ------------------------------------------------------------------------------------------------------------------
namespace imbACE.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using imbACE.Core.core.exceptions;

/**
 * \defgroup ace_ext ACE:Ext Extensions
 * Vast set of tools
 * 
 * \defgroup ace_ext_datastructs ACE:DataStructures
 * \ingroup_disabled ace_ext
 * ACE Data structures extensions
 * 
 * \defgroup ace_ext_collections ACE:CollectionTools
 * \ingroup_disabled ace_ext_datastructs
 * Extensive collection tools
 * exporters, importers, constructors and tools
 * 
 * \defgroup ace_ext_collections_highlight ACE:CollectionTools:Highlight
 * \ingroup_disabled ace_ext_collections
 * 
 * \defgroup ace_ext_strings ACE:StringExtensions
 * \ingroup_disabled ace_ext
 * generators, formats, is tests, selects, string collections
 * 
 * \defgroup ace_ext_strings_generators ACE:String:Generators
 * \ingroup_disabled ace_ext_string
 * Different string generators and formatters
 * 
 * \defgroup ace_ext_types ACE:Types
 * \ingroup_disabled ace_ext
 * Compact typology and object-2-object mapping
 * Describing types and data access and reproduction
 * 
 * 
  *\defgroup ace_ext_path ACE:Path
 * \ingroup_disabled ace_ext
 * Resource, folder, data ... path manipulation, children retrival, etc.
 * 
 * \defgroup ace_ext_filepath ACE:Path:Filepath
 * \ingroup_disabled ace_ext_path
 * Path manipulation and file operations
 * Path methods: parts extraction, file retrieval, pre-writing checks
 * 
 * \defgroup ace_ext_filepath_highlight ACE:Path:Filepath:Highlight
 * \ingroup_disabled ace_ext_filepath
 * The most useful file path methods
 * 
 * \defgroup ace_ext_xpath_highlight ACE:Path:XPath
 * \ingroup_disabled ace_ext_path

 * XPath and other non filepaths manipulation, extraction,modification ... 
 * Path methods: parts extraction, file retrieval, pre-writing checks
 * 
 * \defgroup ace_ext_enum ACE:Enum 
 * \ingroup_disabled ace_ext
 * Enumeration converters and tools
 * From enum to enum, conversions, Enum type members discovery 
 * 
 * \defgroup ace_ext_enum_highlight ACE:Enum:Highlight
 * \ingroup_disabled ace_ext_enum
 * Some interesting extension methods to have in mind
 * 
 * \defgroup ace_ext_etc ACE:ETC
 * \ingroup_disabled ace_ext
 * Group of uncategorized tools
 * 
 * \defgroup ace_highlight ACE:Highlight
 * \ingroup_disabled ace_ext
 * The most usefull methods, classes and structs all around ACE framework
 * 
 * \defgroup ace_ext_type_highlight ACE:Type:Highlight 
 * \ingroup_disabled ace_ext
 * The most usefull Type and Typology related methods among extensions and classes
 */


/*
 *
* * \defgroup reporting_ll_stylers ACE:Report:API01 Styler Extensions
* \ingroup_disabled reporting_ll
* Extensions aimed to provide all API/Format specific services for content styling accoding to the apstract model defined in <c>style</c> model
*  
 * 
 * 
 * */

/**
 * \defgroup reporting ACE:Report
 * Layers of reporting API - as part of __ACE Common Types__ library
 * 
 * Composer level (imbReportingCore)
 * 
 * \defgroup report_ll ACE:Report:API01 Low-Level API
 * \ingroup_disabled reporting
 * Reporting Low-Level API
 * Mostly consisted from Extension static classes, enums and low-level data structs.
 * 
 * + aceColors
 *  + palette manager, palette provider
 * + style
 *  + descriptors: styleSide, styleFontSizeSet
 * + templates
 *  + template fields
 *  + string tempaltes
 * + cursor and zones
*
 * \defgroup report_ll_zone ACE:Report:API01:Zone Zone&Cursor API
 * \ingroup_disabled report_ll
 * Cursor and zones mechanism classes
 * 
 * \defgroup report_ll_style ACE:Report:API01:Style Styles, fonts and colors
 * \ingroup_disabled report_ll
 * 
 * \defgroup report_ll_templates ACE:Report:API01:Template
 * \ingroup_disabled report_ll
 * Staff around template content
 * 
 * \defgroup report_ll_highlights ACE:Report:API01 Important members
 * \ingroup_disabled report_ll
 * The most important methods and classes
 * 
 * \defgroup report_int ACE:Report:API02 Mediator - universal apstraction interface layer
 * \ingroup_disabled reporting
 * Interface apstraction layer
 * Universal API providing format-free / cross platform content generation on procedural.
 * Uni API is described by: ITextRender, IStyleRender and IDocumentBuilder interfaces
 * Next API layer uses: builderForMarkdown, builderForStyle, builderForTableDocument, builderForFlowDocument, builderForText
 * 
 *  * * Interface level (imbACE.Core)
 *  + Render
 *  + Builder
 *  + Stylers
 *
 *
 * 
 * \defgroup report_int_tools ACE:Report:API02:Tools 
 * \ingroup_disabled report_int
 * Set of internally used classes to help the main group.
 * 
 * \defgroup report_cm ACE:Report:API03 Macro layer
 * \ingroup_disabled reporting
 * Procedural and macro layer
 * Macro - composers are executing repetative Interface apstraction layer members reducing coding workload
 *
 * \defgroup report_cm_comp ACE:Report:API03:Composers
 * \ingroup_disabled report_cm
 * Composers are using builders and renders to construct content based on metaContentBase : IMetaContent elements from metaDocumentSet meta model 
 * 
 * 
 * \defgroup report_cm_render ACE:Report:API03:Renders
 * \ingroup_disabled report_cm
 * Renders are build on coresponding IRender Append command apstraction layer
 */

/*
 * 
 * \defgroup report_hl ACE:Report:API04 High-Level API 
 * \ingroup_disabled reporting
 * Reporting High-Level api
 * MetaDocumentSet content high level apstraction model for applicative use.
 * * High-level API (imbReportingCore)
 *  + metaDocument model
 *  + metaContent components
 * 
 * \defgroup report_etc ACE:Report:APIETC
 * \ingroup_disabled reporting
 * Helpers and descriptors
 * Lateral classes, static tools and extensions that are used internaly by reporting API but may become usefull for other applications as well.
 * 
 * \defgroup report_etc_desc ACE:Report:APIETC:Desc Descriptors
 * \ingroup_disabled report_etc
 * Descriptors, definitions and declarations
 * Mostly data objects and declarations. Uded by the API internaly to describe settings and customizations.
 * 
 * \defgroup report_etc_help ACE:Reports:APIETC:Help Helper tools aimed 
 * \ingroup_disabled report_etc
 * Helper extensions and static managers
 * Extensions used internaly and extensions meant for lateral applications. 
 * Static managers provide access to presets and default settings
 * 
 * 
 * \defgroup depricated Depricated - to be removed
 */


    /// <summary>
    /// <para>Set of basic classes, DOMs and APIs shared by various ACE projects.</para>
    /// <para>Universal Source code DOM <see cref="imbACE.Core.data.code"/> and declaration DOM <see cref="imbACE.Core.data.codeSyntax"/>  ....</para>
    /// </summary>
    /// <summary>
    /// <para>Set of basic classes, DOMs and APIs shared by various ACE projects.</para>
    /// <para>Universal Source code DOM <see cref="imbACE.Core.data.code"/> and declaration DOM <see cref="imbACE.Core.data.codeSyntax"/>  ....</para>
    /// </summary>
    [System.Runtime.CompilerServices.CompilerGenerated]
    class NamespaceDoc
    {
    }



}