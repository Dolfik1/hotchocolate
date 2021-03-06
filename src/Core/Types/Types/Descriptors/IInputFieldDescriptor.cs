﻿using HotChocolate.Language;

namespace HotChocolate.Types
{
    public interface IInputFieldDescriptor
        : IFluent
    {
        IInputFieldDescriptor SyntaxNode(InputValueDefinitionNode syntaxNode);

        IInputFieldDescriptor Name(NameString name);

        IInputFieldDescriptor Description(string description);

        IInputFieldDescriptor Type<TInputType>()
            where TInputType : IInputType;

        IInputFieldDescriptor Type<TInputType>(TInputType inputType)
            where TInputType : class, IInputType;

        IInputFieldDescriptor Type(ITypeNode type);

        IInputFieldDescriptor Ignore();

        IInputFieldDescriptor DefaultValue(IValueNode defaultValue);

        IInputFieldDescriptor DefaultValue(object defaultValue);

        IInputFieldDescriptor Directive<T>(T directive)
            where T : class;

        IInputFieldDescriptor Directive<T>()
            where T : class, new();

        IInputFieldDescriptor Directive(
            NameString name,
            params ArgumentNode[] arguments);
    }
}
