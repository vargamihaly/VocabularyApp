﻿// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Design", "CA1040:Avoid empty interfaces", Justification = "Dependency Injection Lifetime", Scope = "type", Target = "~T:VocabularyApp.Common.Core.IScoped")]
[assembly: SuppressMessage("Design", "CA1040:Avoid empty interfaces", Justification = "Dependency Injection Lifetime", Scope = "type", Target = "~T:VocabularyApp.Common.Core.ITransient")]
[assembly: SuppressMessage("Design", "CA1040:Avoid empty interfaces", Justification = "Dependency Injection Lifetime", Scope = "type", Target = "~T:VocabularyApp.Common.Core.ISingleton")]
