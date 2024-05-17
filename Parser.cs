
using System;
using System.IO;
using System.Runtime.Serialization;
using com.calitha.goldparser.lalr;
using com.calitha.commons;
using WindowsFormsApp1;
using System.Windows.Forms;

namespace com.calitha.goldparser
{

    [Serializable()]
    public class SymbolException : System.Exception
    {
        public SymbolException(string message) : base(message)
        {
        }

        public SymbolException(string message,
            Exception inner) : base(message, inner)
        {
        }

        protected SymbolException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

    }

    [Serializable()]
    public class RuleException : System.Exception
    {

        public RuleException(string message) : base(message)
        {
        }

        public RuleException(string message,
                             Exception inner) : base(message, inner)
        {
        }

        protected RuleException(SerializationInfo info,
                                StreamingContext context) : base(info, context)
        {
        }

    }

    enum SymbolConstants : int
    {
        SYMBOL_EOF               =  0, // (EOF)
        SYMBOL_ERROR             =  1, // (Error)
        SYMBOL_WHITESPACE        =  2, // Whitespace
        SYMBOL_MINUS             =  3, // '-'
        SYMBOL_MINUSMINUS        =  4, // '--'
        SYMBOL_NUM               =  5, // '#'
        SYMBOL_LPAREN            =  6, // '('
        SYMBOL_RPAREN            =  7, // ')'
        SYMBOL_TIMES             =  8, // '*'
        SYMBOL_COMMA             =  9, // ','
        SYMBOL_DIV               = 10, // '/'
        SYMBOL_LBRACE            = 11, // '{'
        SYMBOL_RBRACE            = 12, // '}'
        SYMBOL_PLUS              = 13, // '+'
        SYMBOL_PLUSPLUS          = 14, // '++'
        SYMBOL_LT                = 15, // '<'
        SYMBOL_LTEQ              = 16, // '<='
        SYMBOL_EQ                = 17, // '='
        SYMBOL_GT                = 18, // '>'
        SYMBOL_GTEQ              = 19, // '>='
        SYMBOL_BOOL              = 20, // bool
        SYMBOL_CHAR              = 21, // char
        SYMBOL_DEF               = 22, // Def
        SYMBOL_ELIF              = 23, // elif
        SYMBOL_ELSE              = 24, // else
        SYMBOL_FLOAT             = 25, // float
        SYMBOL_FOR               = 26, // for
        SYMBOL_IF                = 27, // if
        SYMBOL_INT               = 28, // int
        SYMBOL_NUMBER            = 29, // Number
        SYMBOL_RETURN            = 30, // return
        SYMBOL_START             = 31, // Start
        SYMBOL_VARIABLE          = 32, // Variable
        SYMBOL_ARGUMENTS         = 33, // <Arguments>
        SYMBOL_ASSIGNMENT        = 34, // <Assignment>
        SYMBOL_COMM              = 35, // <comm>
        SYMBOL_CONDITION         = 36, // <condition>
        SYMBOL_ELSEBLOCK         = 37, // <ElseBlock>
        SYMBOL_EXPRESSION        = 38, // <Expression>
        SYMBOL_FACTOR            = 39, // <Factor>
        SYMBOL_FORLOOP           = 40, // <ForLoop>
        SYMBOL_IFSTATEMENT       = 41, // <IfStatement>
        SYMBOL_METHODCALL        = 42, // <MethodCall>
        SYMBOL_METHODDECLARATION = 43, // <MethodDeclaration>
        SYMBOL_METHODNAME        = 44, // <MethodName>
        SYMBOL_NUMBER2           = 45, // <Number>
        SYMBOL_OP                = 46, // <op>
        SYMBOL_PARAMETER         = 47, // <Parameter>
        SYMBOL_PARAMETERS        = 48, // <Parameters>
        SYMBOL_PROGRAM           = 49, // <program>
        SYMBOL_STATMENT          = 50, // <statment>
        SYMBOL_STATMENTS         = 51, // <Statments>
        SYMBOL_STEPS             = 52, // <steps>
        SYMBOL_TERM              = 53, // <Term>
        SYMBOL_TYPE              = 54, // <Type>
        SYMBOL_VARIABLE2         = 55  // <variable>
    };

    enum RuleConstants : int
    {
        RULE_PROGRAM_START_LBRACE_RBRACE                              =  0, // <program> ::= Start '{' <Statments> '}'
        RULE_STATMENTS                                                =  1, // <Statments> ::= <comm>
        RULE_STATMENTS2                                               =  2, // <Statments> ::= <statment>
        RULE_STATMENTS3                                               =  3, // <Statments> ::= <statment> <Statments>
        RULE_COMM_NUM                                                 =  4, // <comm> ::= '#' <variable>
        RULE_STATMENT                                                 =  5, // <statment> ::= <Assignment>
        RULE_STATMENT2                                                =  6, // <statment> ::= <IfStatement>
        RULE_STATMENT3                                                =  7, // <statment> ::= <ForLoop>
        RULE_STATMENT4                                                =  8, // <statment> ::= <MethodDeclaration>
        RULE_STATMENT5                                                =  9, // <statment> ::= <MethodCall>
        RULE_ASSIGNMENT_EQ                                            = 10, // <Assignment> ::= <variable> '=' <Expression>
        RULE_VARIABLE_VARIABLE                                        = 11, // <variable> ::= Variable
        RULE_EXPRESSION                                               = 12, // <Expression> ::= <Term>
        RULE_EXPRESSION_PLUS                                          = 13, // <Expression> ::= <Expression> '+' <Term>
        RULE_EXPRESSION_MINUS                                         = 14, // <Expression> ::= <Expression> '-' <Term>
        RULE_TERM                                                     = 15, // <Term> ::= <Factor>
        RULE_TERM_TIMES                                               = 16, // <Term> ::= <Term> '*' <Factor>
        RULE_TERM_DIV                                                 = 17, // <Term> ::= <Term> '/' <Factor>
        RULE_FACTOR                                                   = 18, // <Factor> ::= <Number>
        RULE_FACTOR2                                                  = 19, // <Factor> ::= <variable>
        RULE_FACTOR_LPAREN_RPAREN                                     = 20, // <Factor> ::= '(' <Expression> ')'
        RULE_NUMBER_NUMBER                                            = 21, // <Number> ::= Number
        RULE_IFSTATEMENT_IF_LPAREN_RPAREN_LBRACE_RBRACE               = 22, // <IfStatement> ::= if '(' <condition> ')' '{' <Statments> '}' <ElseBlock>
        RULE_ELSEBLOCK_ELSE_LBRACE_RBRACE                             = 23, // <ElseBlock> ::= else '{' <Statments> '}'
        RULE_ELSEBLOCK_ELIF_LPAREN_RPAREN_LBRACE_RBRACE               = 24, // <ElseBlock> ::= elif '(' <condition> ')' '{' <Statments> '}' <ElseBlock>
        RULE_ELSEBLOCK                                                = 25, // <ElseBlock> ::= 
        RULE_CONDITION                                                = 26, // <condition> ::= <Expression> <op> <Expression>
        RULE_OP_GT                                                    = 27, // <op> ::= '>'
        RULE_OP_LT                                                    = 28, // <op> ::= '<'
        RULE_OP_LTEQ                                                  = 29, // <op> ::= '<='
        RULE_OP_GTEQ                                                  = 30, // <op> ::= '>='
        RULE_FORLOOP_FOR_LPAREN_COMMA_COMMA_RPAREN_LBRACE_RBRACE      = 31, // <ForLoop> ::= for '(' <Assignment> ',' <condition> ',' <steps> ')' '{' <Statments> '}'
        RULE_STEPS_PLUSPLUS                                           = 32, // <steps> ::= '++' <variable>
        RULE_STEPS_MINUSMINUS                                         = 33, // <steps> ::= '--' <variable>
        RULE_STEPS_MINUSMINUS2                                        = 34, // <steps> ::= <variable> '--'
        RULE_STEPS_PLUSPLUS2                                          = 35, // <steps> ::= <variable> '++'
        RULE_METHODDECLARATION_DEF_LPAREN_RPAREN_LBRACE_RETURN_RBRACE = 36, // <MethodDeclaration> ::= Def <MethodName> '(' <Parameters> ')' '{' <Statments> return <Expression> '}'
        RULE_METHODNAME                                               = 37, // <MethodName> ::= <variable>
        RULE_PARAMETERS                                               = 38, // <Parameters> ::= <Parameter>
        RULE_PARAMETERS_COMMA                                         = 39, // <Parameters> ::= <Parameter> ',' <Parameters>
        RULE_PARAMETER                                                = 40, // <Parameter> ::= <Type> <variable>
        RULE_TYPE_INT                                                 = 41, // <Type> ::= int
        RULE_TYPE_FLOAT                                               = 42, // <Type> ::= float
        RULE_TYPE_CHAR                                                = 43, // <Type> ::= char
        RULE_TYPE_BOOL                                                = 44, // <Type> ::= bool
        RULE_METHODCALL_LPAREN_RPAREN                                 = 45, // <MethodCall> ::= <MethodName> '(' <Arguments> ')'
        RULE_ARGUMENTS                                                = 46, // <Arguments> ::= <Expression>
        RULE_ARGUMENTS_COMMA                                          = 47  // <Arguments> ::= <Expression> ',' <Arguments>
    };

    public class MyParser
    {
        public ListBox lst1;
        public ListBox lst2;
        private LALRParser parser;

        public MyParser(string filename, ListBox lst1, ListBox lst2)
        {
            FileStream stream = new FileStream(filename,
                                               FileMode.Open, 
                                               FileAccess.Read, 
                                               FileShare.Read);
            this.lst1 = lst1;
            this.lst2 = lst2;
            Init(stream);
            stream.Close();
        }

        public MyParser(string baseName, string resourceName)
        {
            byte[] buffer = ResourceUtil.GetByteArrayResource(
                System.Reflection.Assembly.GetExecutingAssembly(),
                baseName,
                resourceName);
            MemoryStream stream = new MemoryStream(buffer);
            Init(stream);
            stream.Close();
        }

        public MyParser(Stream stream)
        {
            Init(stream);
        }

        private void Init(Stream stream)
        {
            CGTReader reader = new CGTReader(stream);
            parser = reader.CreateNewParser();
            parser.TrimReductions = false;
            parser.StoreTokens = LALRParser.StoreTokensMode.NoUserObject;

            parser.OnTokenError += new LALRParser.TokenErrorHandler(TokenErrorEvent);
            parser.OnParseError += new LALRParser.ParseErrorHandler(ParseErrorEvent);
            parser.OnTokenRead += new LALRParser.TokenReadHandler(OnTokenRead);
        }

        public void Parse(string source)
        {
            NonterminalToken token = parser.Parse(source);
            if (token != null)
            {
                Object obj = CreateObject(token);
                //todo: Use your object any way you like
            }
        }

        private Object CreateObject(Token token)
        {
            if (token is TerminalToken)
                return CreateObjectFromTerminal((TerminalToken)token);
            else
                return CreateObjectFromNonterminal((NonterminalToken)token);
        }

        private Object CreateObjectFromTerminal(TerminalToken token)
        {
            switch (token.Symbol.Id)
            {
                case (int)SymbolConstants.SYMBOL_EOF :
                //(EOF)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ERROR :
                //(Error)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHITESPACE :
                //Whitespace
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUS :
                //'-'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUSMINUS :
                //'--'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_NUM :
                //'#'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LPAREN :
                //'('
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RPAREN :
                //')'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMES :
                //'*'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COMMA :
                //','
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIV :
                //'/'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LBRACE :
                //'{'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RBRACE :
                //'}'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUS :
                //'+'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUSPLUS :
                //'++'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LT :
                //'<'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LTEQ :
                //'<='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQ :
                //'='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GT :
                //'>'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GTEQ :
                //'>='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_BOOL :
                //bool
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CHAR :
                //char
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DEF :
                //Def
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ELIF :
                //elif
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ELSE :
                //else
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FLOAT :
                //float
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR :
                //for
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF :
                //if
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_INT :
                //int
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_NUMBER :
                //Number
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RETURN :
                //return
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_START :
                //Start
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_VARIABLE :
                //Variable
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ARGUMENTS :
                //<Arguments>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ASSIGNMENT :
                //<Assignment>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COMM :
                //<comm>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_CONDITION :
                //<condition>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ELSEBLOCK :
                //<ElseBlock>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXPRESSION :
                //<Expression>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FACTOR :
                //<Factor>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FORLOOP :
                //<ForLoop>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IFSTATEMENT :
                //<IfStatement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_METHODCALL :
                //<MethodCall>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_METHODDECLARATION :
                //<MethodDeclaration>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_METHODNAME :
                //<MethodName>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_NUMBER2 :
                //<Number>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_OP :
                //<op>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PARAMETER :
                //<Parameter>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PARAMETERS :
                //<Parameters>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PROGRAM :
                //<program>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STATMENT :
                //<statment>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STATMENTS :
                //<Statments>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STEPS :
                //<steps>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TERM :
                //<Term>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TYPE :
                //<Type>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_VARIABLE2 :
                //<variable>
                //todo: Create a new object that corresponds to the symbol
                return null;

            }
            throw new SymbolException("Unknown symbol");
        }

        public Object CreateObjectFromNonterminal(NonterminalToken token)
        {
            switch (token.Rule.Id)
            {
                case (int)RuleConstants.RULE_PROGRAM_START_LBRACE_RBRACE :
                //<program> ::= Start '{' <Statments> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATMENTS :
                //<Statments> ::= <comm>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATMENTS2 :
                //<Statments> ::= <statment>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATMENTS3 :
                //<Statments> ::= <statment> <Statments>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_COMM_NUM :
                //<comm> ::= '#' <variable>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATMENT :
                //<statment> ::= <Assignment>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATMENT2 :
                //<statment> ::= <IfStatement>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATMENT3 :
                //<statment> ::= <ForLoop>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATMENT4 :
                //<statment> ::= <MethodDeclaration>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATMENT5 :
                //<statment> ::= <MethodCall>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ASSIGNMENT_EQ :
                //<Assignment> ::= <variable> '=' <Expression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_VARIABLE_VARIABLE :
                //<variable> ::= Variable
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION :
                //<Expression> ::= <Term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION_PLUS :
                //<Expression> ::= <Expression> '+' <Term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION_MINUS :
                //<Expression> ::= <Expression> '-' <Term>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM :
                //<Term> ::= <Factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_TIMES :
                //<Term> ::= <Term> '*' <Factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TERM_DIV :
                //<Term> ::= <Term> '/' <Factor>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR :
                //<Factor> ::= <Number>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR2 :
                //<Factor> ::= <variable>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FACTOR_LPAREN_RPAREN :
                //<Factor> ::= '(' <Expression> ')'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_NUMBER_NUMBER :
                //<Number> ::= Number
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IFSTATEMENT_IF_LPAREN_RPAREN_LBRACE_RBRACE :
                //<IfStatement> ::= if '(' <condition> ')' '{' <Statments> '}' <ElseBlock>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ELSEBLOCK_ELSE_LBRACE_RBRACE :
                //<ElseBlock> ::= else '{' <Statments> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ELSEBLOCK_ELIF_LPAREN_RPAREN_LBRACE_RBRACE :
                //<ElseBlock> ::= elif '(' <condition> ')' '{' <Statments> '}' <ElseBlock>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ELSEBLOCK :
                //<ElseBlock> ::= 
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_CONDITION :
                //<condition> ::= <Expression> <op> <Expression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_GT :
                //<op> ::= '>'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_LT :
                //<op> ::= '<'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_LTEQ :
                //<op> ::= '<='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_OP_GTEQ :
                //<op> ::= '>='
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORLOOP_FOR_LPAREN_COMMA_COMMA_RPAREN_LBRACE_RBRACE :
                //<ForLoop> ::= for '(' <Assignment> ',' <condition> ',' <steps> ')' '{' <Statments> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEPS_PLUSPLUS :
                //<steps> ::= '++' <variable>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEPS_MINUSMINUS :
                //<steps> ::= '--' <variable>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEPS_MINUSMINUS2 :
                //<steps> ::= <variable> '--'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STEPS_PLUSPLUS2 :
                //<steps> ::= <variable> '++'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_METHODDECLARATION_DEF_LPAREN_RPAREN_LBRACE_RETURN_RBRACE :
                //<MethodDeclaration> ::= Def <MethodName> '(' <Parameters> ')' '{' <Statments> return <Expression> '}'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_METHODNAME :
                //<MethodName> ::= <variable>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PARAMETERS :
                //<Parameters> ::= <Parameter>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PARAMETERS_COMMA :
                //<Parameters> ::= <Parameter> ',' <Parameters>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_PARAMETER :
                //<Parameter> ::= <Type> <variable>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TYPE_INT :
                //<Type> ::= int
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TYPE_FLOAT :
                //<Type> ::= float
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TYPE_CHAR :
                //<Type> ::= char
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_TYPE_BOOL :
                //<Type> ::= bool
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_METHODCALL_LPAREN_RPAREN :
                //<MethodCall> ::= <MethodName> '(' <Arguments> ')'
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ARGUMENTS :
                //<Arguments> ::= <Expression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ARGUMENTS_COMMA :
                //<Arguments> ::= <Expression> ',' <Arguments>
                //todo: Create a new object using the stored tokens.
                return null;

            }
            throw new RuleException("Unknown rule");
        }

        private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
        {
            string message = "Token error with input: '"+args.Token.ToString()+"'";
            //todo: Report message to UI?
        }

        private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {
            string message = "Parse error caused by token: '"+args.UnexpectedToken.ToString()+"' in line "+ args.UnexpectedToken.Location.LineNr;
            lst1.Items.Add(message);
            string message2 = "Expected: '" + args.ExpectedTokens.ToString();
            lst1.Items.Add(message2);
            //todo: Report message to UI?
        }

        private void OnTokenRead(LALRParser parser, TokenReadEventArgs args)
        {
            string message =args.Token.Text + "     \t\t" + (SymbolConstants)args.Token.Symbol.Id;
            lst2.Items.Add(message);
            //todo: Report message to UI?
        }

    }
}
