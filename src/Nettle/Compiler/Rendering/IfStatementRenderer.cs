﻿namespace Nettle.Compiler.Rendering
{
    using Nettle.Compiler.Parsing.Blocks;
    using Nettle.Functions;
    using System;

    /// <summary>
    /// Represents an if statement renderer
    /// </summary>
    internal class IfStatementRenderer : NettleRenderer, IBlockRenderer
    {
        private BooleanExpressionEvaluator _expressionEvaluator;
        private BlockCollectionRenderer _collectionRenderer;

        /// <summary>
        /// Constructs the renderer with required dependencies
        /// </summary>
        /// <param name="functionRepository">The function repository</param>
        /// <param name="expressionEvaluator">The expression evaluator</param>
        /// <param name="collectionRenderer">The block collection renderer</param>
        public IfStatementRenderer
            (
                IFunctionRepository functionRepository,
                BooleanExpressionEvaluator expressionEvaluator,
                BlockCollectionRenderer collectionRenderer
            )

            : base(functionRepository)
        {
            Validate.IsNotNull(expressionEvaluator);
            Validate.IsNotNull(collectionRenderer);

            _expressionEvaluator = expressionEvaluator;
            _collectionRenderer = collectionRenderer;
        }

        /// <summary>
        /// Determines if the renderer can render the code block specified
        /// </summary>
        /// <param name="block">The code block</param>
        /// <returns>True, if it can be rendered; otherwise false</returns>
        public bool CanRender
            (
                CodeBlock block
            )
        {
            Validate.IsNotNull(block);

            var blockType = block.GetType();

            return
            (
                blockType == typeof(IfStatement)
            );
        }

        /// <summary>
        /// Renders the code block specified into a string
        /// </summary>
        /// <param name="context">The template context</param>
        /// <param name="block">The code block to render</param>
        /// <param name="flags">The template flags</param>
        /// <returns>The rendered block</returns>
        public string Render
            (
                ref TemplateContext context,
                CodeBlock block,
                params TemplateFlag[] flags
            )
        {
            Validate.IsNotNull(block);

            var statement = (IfStatement)block;
            
            var result = _expressionEvaluator.Evaluate
            (
                ref context,
                statement.ConditionExpression
            );

            if (false == result)
            {
                return String.Empty;
            }
            else
            {
                var renderedBody = _collectionRenderer.Render
                (
                    ref context,
                    statement.Blocks,
                    flags
                );

                return renderedBody;
            }
        }
    }
}
