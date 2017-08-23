﻿namespace Nettle.Compiler.Parsing
{
    using Nettle.Compiler.Parsing.Blocks;
    using System;

    /// <summary>
    /// Represents an if statement code block parser
    /// </summary>
    internal sealed class IfStatementParser : NestedBlockParser<IfStatement>
    {
        /// <summary>
        /// Constructs the parser with a blockifier
        /// </summary>
        /// <param name="blockifier">The blockifier</param>
        public IfStatementParser
            (
                IBlockifier blockifier
            )

            : base(blockifier)
        { }

        /// <summary>
        /// Parses the 'if statement' signature into a code block object
        /// </summary>
        /// <param name="templateContent">The template content</param>
        /// <param name="positionOffSet">The position offset index</param>
        /// <param name="signature">The variable signature</param>
        /// <returns>The parsed if statement</returns>
        public override IfStatement Parse
            (
                ref string templateContent,
                ref int positionOffSet,
                string signature
            )
        {
            var ifStatement = UnwrapSignatureBody(signature);

            var conditionName = ifStatement.RightOf
            (
                "if "
            );

            if (String.IsNullOrWhiteSpace(conditionName))
            {
                throw new NettleParseException
                (
                    "The if statements condition name must be specified.",
                    positionOffSet
                );
            }

            var conditionType = ResolveType
            (
                conditionName
            );

            var nestedBody = ExtractNestedBody
            (
                ref templateContent,
                ref positionOffSet,
                signature,
                "foreach",
                "endfor"
            );

            return new IfStatement()
            {
                Signature = nestedBody.Signature,
                StartPosition = nestedBody.StartPosition,
                EndPosition = nestedBody.EndPosition,
                ConditionName = conditionName,
                ConditionType = conditionType,
                Body = nestedBody.Body,
                Blocks = nestedBody.Blocks
            };
        }
    }
}