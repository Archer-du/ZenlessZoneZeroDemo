# ZZZDemo

---

## Log

- 解决的问题
  - 主更新逻辑没有分好阶段，执行顺序乱七八糟
  - model层不应该依赖平台引擎api，而是将需要的api分别抽象到各个功能模块业务逻辑的接口中由引擎实现。之所以如此设计model层，不只是模块复用，更是为了保证实现完全为了需求服务

## Locomotion

- 平滑旋转移动

