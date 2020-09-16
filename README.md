# Endpoints.Framework

## 功能

#### 基础类型相关

- ArrayExtensions：扩展数组方法，使其支持ECMAScript6的数组常用方法

- IEnumerableExtensions：扩展IEnumerable，使其支持ECMAScript6的数组常用方法

- StringExtensions：扩展字符串类型，添加正则相关操作

#### WebApi相关

- ApiResponseFilter：为dotnet core提供统一的返回值过滤器

#### 鉴权相关

- JwtGenerator：通过简单的方式生成Json Web Token

- PrefixHook：允许你通过Hook Bearer的方法实现自定义Authorization的校验前缀

#### 参数校验相关

- IsMd5Attribute：Md5参数校验

