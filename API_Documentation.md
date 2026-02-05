# RF-Client API 服务器文档

基于客户端源代码分析的 API 接口文档

---

## 基础信息

### 服务器地址
- **Debug/Release**: `https://ra2yr.dreamcloud.top:9999/`
- 配置位置: `ClientCore/Settings/NetWorkINISettings.cs:26-30`

### 请求格式
- **认证方式**: Bearer Token (在 Authorization Header 中)
- **Content-Type**: `application/json` (POST/PUT请求)
- **超时时间**: 默认 30 秒，可配置

### 通用响应格式
所有 API 响应都遵循以下格式:

```json
{
  "code": "200",
  "message": "操作成功",
  "data": {}
}
```

- `code`: 状态码 (200=成功, 其他=失败)
- `message`: 操作结果描述
- `data`: 实际数据，可为对象、数组或基本类型

---

## API 接口列表

### 1. 用户管理

#### 1.1 用户登录
- **接口**: `POST /user/login`
- **请求体**:
```json
{
  "name": "player1",
  "pwd": "password123"
}
```
- **完整响应**:
```json
{
  "code": "200",
  "message": "登录成功",
  "data": {
    "access_token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    "user": {
      "id": "12345",
      "username": "player1",
      "email": "player@example.com",
      "role": "user",
      "side": 1,
      "badge": "5",
      "tag": "VIP",
      "mac": null
    }
  }
}
```

#### 1.2 Token登录
- **接口**: `POST /user/loginByToken`
- **请求体**:
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "mac": "00:1A:2B:3C:4D:5E"
}
```
- **完整响应**:
```json
{
  "code": "200",
  "message": "登录成功",
  "data": {
    "access_token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    "user": {
      "id": "12345",
      "username": "player1",
      "email": "player@example.com",
      "role": "user",
      "side": 1,
      "badge": "5",
      "tag": "VIP"
    }
  }
}
```

#### 1.3 用户注册
- **接口**: `POST /user/register`
- **请求体**:
```json
{
  "username": "newplayer",
  "password": "password123",
  "email": "newplayer@example.com",
  "side": 3,
  "tag": ""
}
```
- **完整响应**:
```json
{
  "code": "200",
  "message": "注册成功",
  "data": true
}
```

#### 1.4 重置密码
- **接口**: `POST /user/reset`
- **请求体**:
```json
{
  "email": "player@example.com",
  "username": "player1"
}
```
- **完整响应**:
```json
{
  "code": "200",
  "message": "重置密码邮件已发送",
  "data": true
}
```

#### 1.5 修改密码
- **接口**: `POST /user/changePassword`
- **请求体**:
```json
{
  "id": "12345",
  "oldPwd": "oldpass123",
  "newPwd": "newpass456"
}
```
- **完整响应**:
```json
{
  "code": "200",
  "message": "密码修改成功",
  "data": true
}
```

#### 1.6 更新用户信息
- **接口**: `POST /user/editUser`
- **请求体**:
```json
{
  "id": "12345",
  "username": "player1",
  "email": "newemail@example.com",
  "side": 1,
  "badge": "5",
  "tag": "VIP"
}
```
- **完整响应**:
```json
{
  "code": "200",
  "message": "用户信息更新成功",
  "data": true
}
```

#### 1.7 获取注册验证码
- **接口**: `GET /user/getSignCode?email={email}`
- **完整响应**:
```json
{
  "code": "200",
  "message": "验证码已发送",
  "data": "123456"
}
```

#### 1.8 获取重置密码验证码
- **接口**: `GET /user/getResetCode?email={email}`
- **完整响应**:
```json
{
  "code": "200",
  "message": "验证码已发送",
  "data": "654321"
}
```

#### 1.9 更新用户徽章
- **接口**: `POST /user/updUserBadge`
- **请求体**:
```json
{
  "userId": "12345",
  "badgeId": "5"
}
```
- **完整响应**:
```json
{
  "code": "200",
  "message": "徽章更新成功",
  "data": true
}
```

#### 1.10 获取用户经验值
- **接口**: `GET /user/getUserExp?userId={userId}`
- **完整响应**:
```json
{
  "code": "200",
  "message": "获取成功",
  "data": {
    "exp": 1250,
    "nextLevelExp": 2000,
    "level": 5,
    "badgeName": "老兵徽章",
    "canUseBadges": [
      {
        "id": "1",
        "name": "新手徽章",
        "side": 0,
        "level": 1
      },
      {
        "id": "2",
        "name": "精英徽章",
        "side": 1,
        "level": 3
      },
      {
        "id": "5",
        "name": "老兵徽章",
        "side": 2,
        "level": 5
      }
    ]
  }
}
```

#### 1.11 提交答题分数
- **接口**: `POST /user/pass`
- **请求体**:
```json
{
  "userId": "12345",
  "score": 95
}
```
- **完整响应**:
```json
{
  "code": "200",
  "message": "分数提交成功",
  "data": true
}
```

#### 1.12 检查用户MAC是否被封禁
- **接口**: `GET /user/checkBanUserByMac?mac={mac}`
- **说明**: 根据MAC地址检查用户是否被封禁，客户端启动时会调用此接口
- **请求示例**: `GET /user/checkBanUserByMac?mac=00:1A:2B:3C:4D:5E`
- **完整响应（未封禁）**:
```json
{
  "code": "200",
  "message": "用户正常",
  "data": false
}
```
- **完整响应（已封禁）**:
```json
{
  "code": "200",
  "message": "用户已被封禁",
  "data": true
}
```

---

### 2. 组件管理 (创意工坊)

#### 2.1 获取用户所有组件
- **接口**: `GET /component/getAllComponent?userId={userId}&type={type}`
- **完整响应**:
```json
{
  "code": "200",
  "message": "获取成功",
  "data": [
    {
      "id": "comp001",
      "name": "超级武器模组",
      "description": "添加新的超级武器单位",
      "type": 1,
      "tags": "武器,平衡",
      "file": "files/mod1.zip",
      "size": 10485760,
      "hash": "abc123def456",
      "uploadTime": "2024-01-15 10:30:00",
      "uploadUser": "user123",
      "passTime": "2024-01-16 14:20:00",
      "uploadUserName": "modder1",
      "typeName": "单位模组",
      "downCount": 1523,
      "version": "1.2.0",
      "apply": "1.5",
      "author": "Modder1"
    }
  ]
}
```

#### 2.2 获取审核通过的组件
- **接口**: `GET /component/getAuditComponent`
- **完整响应**:
```json
{
  "code": "200",
  "message": "获取成功",
  "data": [
    {
      "id": "comp002",
      "name": "新战役包",
      "description": "包含10个新任务",
      "type": 2,
      "tags": "战役,官方",
      "file": "files/campaign2.zip",
      "size": 52428800,
      "hash": "def789ghi012",
      "uploadTime": "2024-02-01 09:00:00",
      "uploadUser": "admin",
      "passTime": "2024-02-02 10:00:00",
      "uploadUserName": "官方制作组",
      "typeName": "战役包",
      "downCount": 8547,
      "version": "2.0.1",
      "apply": "1.6",
      "author": "官方团队"
    }
  ]
}
```

#### 2.3 获取待审核组件
- **接口**: `GET /component/getUnAuditComponent`
- **完整响应**:
```json
{
  "code": "200",
  "message": "获取成功",
  "data": [
    {
      "id": "comp003",
      "name": "测试模组",
      "description": "等待审核",
      "type": 1,
      "tags": "测试",
      "file": "files/test.zip",
      "size": 2097152,
      "hash": "test123",
      "uploadTime": "2024-03-01 12:00:00",
      "uploadUser": "user456",
      "passTime": null,
      "uploadUserName": "测试用户",
      "typeName": "单位模组",
      "downCount": 0,
      "version": "0.1",
      "apply": "1.5",
      "author": "测试者"
    }
  ]
}
```

#### 2.4 获取组件下载URL
- **接口**: `GET /component/getComponentUrl?id={id}`
- **完整响应**:
```json
{
  "code": "200",
  "message": "获取成功",
  "data": "https://ra2yr.dreamcloud.top:9999/files/download/comp001?token=xxx"
}
```

#### 2.5 添加组件
- **接口**: `POST /component/addComponent`
- **请求体**: `MultipartFormDataContent`
  - `name`: "超级武器模组"
  - `description`: "添加新的超级武器"
  - `type`: 1
  - `tags`: "武器,平衡"
  - `file`: (文件)
  - `version`: "1.0.0"
  - `apply`: "1.5"
  - `author`: "作者名"
- **完整响应**:
```json
{
  "code": "200",
  "message": "组件添加成功，等待审核",
  "data": true
}
```

#### 2.6 更新组件
- **接口**: `POST /component/updComponent`
- **请求体**: `MultipartFormDataContent`
  - `id`: "comp001"
  - `name`: "超级武器模组 v2"
  - `description`: "更新后的描述"
  - `type`: 1
  - `file`: (新文件)
  - `version`: "2.0.0"
- **完整响应**:
```json
{
  "code": "200",
  "message": "组件更新成功",
  "data": true
}
```

#### 2.7 删除组件
- **接口**: `POST /component/delComponent`
- **请求体**:
```json
{
  "id": "comp001"
}
```
- **完整响应**:
```json
{
  "code": "200",
  "message": "组件删除成功",
  "data": true
}
```

---

### 3. 题库管理

#### 3.1 获取题库列表
- **接口**: `GET /questionBank/getQuestionBank?types={types}`
- **完整响应**:
```json
{
  "code": "200",
  "message": "获取成功",
  "data": [
    {
      "id": "q001",
      "name": "单位知识",
      "problem": "磁暴步兵的克星是什么单位？",
      "options": "美国大兵,动员兵,磁暴步兵,灰熊坦克",
      "answer": 1,
      "difficulty": 1,
      "type": "单位",
      "enable": 1
    },
    {
      "id": "q002",
      "name": "建筑知识",
      "problem": "苏联的基地车展开后是什么建筑？",
      "options": "建造场,兵营,战车工厂,电厂",
      "answer": 0,
      "difficulty": 1,
      "type": "建筑",
      "enable": 1
    }
  ]
}
```

#### 3.2 获取用户题库
- **接口**: `GET /questionBank/getQuestionBankByUserID?id={userId}`
- **完整响应**:
```json
{
  "code": "200",
  "message": "获取成功",
  "data": [
    {
      "id": "q003",
      "name": "自定义题目1",
      "problem": "这道题的答案是？",
      "options": "A,B,C,D",
      "answer": 2,
      "difficulty": 2,
      "type": "综合",
      "enable": 1
    }
  ]
}
```

#### 3.3 添加题目
- **接口**: `POST /questionBank/addQuestionBank`
- **请求体**:
```json
{
  "name": "战术知识",
  "problem": "在遭遇战中，最快获取资金的方法是？",
  "options": "采集矿石,占领科技油田,卖掉建筑,获取支援",
  "answer": 1,
  "difficulty": 3,
  "type": "战术",
  "enable": 1
}
```
- **完整响应**:
```json
{
  "code": "200",
  "message": "题目添加成功",
  "data": true
}
```

#### 3.4 更新题目
- **接口**: `POST /questionBank/updQuestionBank`
- **请求体**:
```json
{
  "id": "q001",
  "name": "单位知识（已更新）",
  "problem": "磁暴步兵的克星是什么单位？",
  "options": "美国大兵,动员兵,磁暴步兵,灰熊坦克",
  "answer": 1,
  "difficulty": 2,
  "type": "单位",
  "enable": 1
}
```
- **完整响应**:
```json
{
  "code": "200",
  "message": "题目更新成功",
  "data": true
}
```

#### 3.5 删除题目
- **接口**: `POST /questionBank/delQuestionBank`
- **请求体**:
```json
{
  "id": "q001"
}
```
- **完整响应**:
```json
{
  "code": "200",
  "message": "题目删除成功",
  "data": true
}
```

---

### 4. 地图管理

#### 4.1 获取地图信息
- **接口**: `GET /map/getMapInfo?id={id}`
- **完整响应**:
```json
{
  "code": "200",
  "message": "获取成功",
  "data": {
    "tx": 130,
    "img": "https://example.com/maps/map001.png",
    "csf": "MAP0001",
    "id": "map001",
    "name": "沙漠突袭",
    "maxPlayers": "4",
    "author": "MapMaker",
    "type": 1,
    "base64": null,
    "file": "maps/desert_raid.map",
    "otherFile": null,
    "createTime": "2024-01-10 15:30:00",
    "updateTime": "2024-02-20 10:15:00",
    "createUser": "user789",
    "enable": 1,
    "downCount": "3256",
    "score": 4.5,
    "description": "一张适合4人游戏的沙漠地图",
    "sha1": "abc123def456789012345678901234567890abcd",
    "uploadUserName": "MapMaker",
    "typeName": "遭遇战",
    "rules": "标准",
    "ares": 2,
    "enemyHouse": "",
    "allyHouse": "",
    "autoStart": false
  }
}
```

#### 4.2 分页获取地图列表
- **接口**: `GET /map/getRelMapsByPage?search={search}&types={types}&maxPlayers={maxPlayers}&pageNum={pageNum}&pageSize={pageSize}`
- **完整响应**:
```json
{
  "code": "200",
  "message": "获取成功",
  "data": {
    "records": [
      {
        "tx": 130,
        "img": "https://example.com/maps/map001.png",
        "id": "map001",
        "name": "沙漠突袭",
        "maxPlayers": "4",
        "author": "MapMaker",
        "type": 1,
        "score": 4.5,
        "downCount": "3256",
        "description": "一张适合4人游戏的沙漠地图",
        "sha1": "abc123def456",
        "uploadUserName": "MapMaker",
        "typeName": "遭遇战"
      },
      {
        "tx": 132,
        "img": "https://example.com/maps/map002.png",
        "id": "map002",
        "name": "城市争夺",
        "maxPlayers": "6",
        "author": "CityMapper",
        "type": 1,
        "score": 4.2,
        "downCount": "2145",
        "description": "城市环境的大型地图",
        "sha1": "def456ghi789",
        "uploadUserName": "CityMapper",
        "typeName": "遭遇战"
      }
    ],
    "total": 156,
    "size": 20,
    "current": 1,
    "pages": 8
  }
}
```

#### 4.3 上传自定义地图
- **接口**: `POST /custom_map/upload`
- **请求体**: `MultipartFormDataContent`
  - `fileMame`: "my_custom_map.map"
  - `file`: (地图文件)
- **完整响应**:
```json
{
  "code": "200",
  "message": "地图上传成功",
  "data": "fedcba9876543210fedcba9876543210fedcba98"
}
```

---

### 5. 更新服务器管理

#### 5.1 获取所有更新服务器
- **接口**: `GET /updaterServer/getAllUpdaterServer`
- **完整响应**:
```json
{
  "code": "200",
  "message": "获取成功",
  "data": [
    {
      "id": "server1",
      "name": "官方主服务器",
      "type": 0,
      "location": "中国大陆",
      "url": "https://update.ra2cn.com/",
      "priority": 100
    },
    {
      "id": "server2",
      "name": "备用服务器",
      "type": 0,
      "location": "中国香港",
      "url": "https://update2.ra2cn.com/",
      "priority": 80
    },
    {
      "id": "server3",
      "name": "测试版服务器",
      "type": 1,
      "location": "中国大陆",
      "url": "https://update-test.ra2cn.com/",
      "priority": 50
    }
  ]
}
```

#### 5.2 获取最新版本信息
- **接口**: `GET /updater/getNewLatestInfoByBaseVersion?type={type}&baseVersion={baseVersion}`
- **完整响应**:
```json
{
  "code": "200",
  "message": "获取成功",
  "data": {
    "id": "update001",
    "version": "1.6.0.256",
    "file": "update_1.6.0.256.zip",
    "hash": "a1b2c3d4e5f6a1b2c3d4e5f6a1b2c3d4e5f6a1b",
    "size": "157286400",
    "log": "1. 修复了多个bug\\n2. 新增战役包功能\\n3. 优化网络连接",
    "channel": "0",
    "updateTime": "2024-03-15 14:30:00"
  }
}
```

---

### 6. 战役包管理

#### 6.1 分页获取战役包列表
- **接口**: `GET /mission_pack/page?current={current}&size={size}&search={search}&camp={camp}&tags={tags}&year={year}&ares={ares}&phobos={phobos}&tx={tx}&difficulty={difficulty}&gameType={gameType}`
- **完整响应**:
```json
{
  "code": "200",
  "message": "获取成功",
  "data": {
    "records": [
      {
        "id": "mp001",
        "name": "红色风暴",
        "description": "苏联阵营战役包，包含8个任务",
        "camp": [1],
        "tags": ["战役", "官方", "中等难度"],
        "file": "mission_packs/red_storm.zip",
        "imgs": [
          "https://example.com/mp/img1.jpg",
          "https://example.com/mp/img2.jpg"
        ],
        "author": "官方制作组",
        "missionCount": 8,
        "year": 2023,
        "ares": 2,
        "phobos": 0,
        "tx": 1,
        "difficulty": 3,
        "gameType": 1,
        "link": "https://moddb.com/redstorm",
        "updateTime": "2024-01-20 16:00:00"
      }
    ],
    "total": 45,
    "size": 20,
    "current": 1,
    "pages": 3
  }
}
```

#### 6.2 上传战役包图片
- **接口**: `POST /mission_pack/updateImg`
- **请求体**: `MultipartFormDataContent`
  - `id`: "mp001"
  - `file`: (图片文件)
- **完整响应**:
```json
{
  "code": "200",
  "message": "图片上传成功",
  "data": true
}
```

---

### 7. 分数管理 (战役)

#### 7.1 获取分数
- **接口**: `GET /score/getScore?name={name}&missionPack={missionPack}`
- **完整响应**:
```json
{
  "code": "200",
  "message": "获取成功",
  "data": {
    "name": "Player1",
    "score": 85.5,
    "brief": "完成了6/8个任务",
    "missionPack": "mp001",
    "total": 8
  }
}
```

#### 7.2 更新分数
- **接口**: `POST /score/updateScore`
- **请求体**:
```json
{
  "name": "Player1",
  "score": 90.0,
  "brief": "完成了7/8个任务",
  "missionPack": "mp001",
  "total": 8
}
```
- **完整响应**:
```json
{
  "code": "200",
  "message": "分数更新成功",
  "data": true
}
```

---

### 8. 聊天室

#### 8.1 获取在线人数
- **接口**: `GET /ChatRoom/getPeopleCount`
- **完整响应**:
```json
{
  "code": "200",
  "message": "获取成功",
  "data": 1247
}
```

---

### 9. 公告管理

#### 9.1 获取公告
- **接口**: `GET /anno/getAnnoByType?type=mainMenu`
- **完整响应**:
```json
{
  "code": "200",
  "message": "获取成功",
  "data": "欢迎来到红色警戒2重聚版！\\n\\n最新更新：\\n1. 新增战役包功能\\n2. 优化了多人游戏网络\\n3. 修复了若干bug\\n\\n祝游戏愉快！"
}
```

---

### 10. 致谢列表

#### 10.1 获取所有致谢
- **接口**: `GET /thanks/getAllThanks`
- **完整响应**:
```json
{
  "code": "200",
  "message": "获取成功",
  "data": [
    {
      "id": "thank001",
      "author": "PlayerOne",
      "content": "感谢制作组让这款经典游戏重现生机！"
    },
    {
      "id": "thank002",
      "author": "ModderXX",
      "content": "感谢社区提供的工具和支持，让模组制作变得简单。"
    },
    {
      "id": "thank003",
      "author": "OldPlayer",
      "content": "找回了很多年前的回忆，谢谢你们的辛勤付出！"
    }
  ]
}
```

---

### 11. 字典管理

#### 11.1 获取字典值
- **接口**: `GET /dict/getValue?section={section}&key={key}`
- **常见参数**:
  - `section=map, key=type`: 地图类型列表
  - `section=component, key=type`: 组件类型列表
  - `section=user, key=side`: 用户阵营列表
  - `section=question, key=type`: 题目类型列表

**示例1：获取地图类型**
- **请求**: `GET /dict/getValue?section=map&key=type`
- **完整响应**:
```json
{
  "code": "200",
  "message": "获取成功",
  "data": "遭遇战,抢夺地盘,巨款互换,海战,小规模冲突,占领地盘,非作战"
}
```

**示例2：获取组件类型**
- **请求**: `GET /dict/getValue?section=component&key=type`
- **完整响应**:
```json
{
  "code": "200",
  "message": "获取成功",
  "data": "单位模组,建筑模组,界面美化,音效包,战役包,工具"
}
```

---

### 12. 文件下载

#### 12.1 下载文件
- **接口**: `GET /{fileUrl}`
- **说明**: 直接下载文件，支持 Token 认证
- **响应**: 文件流

---

## 数据模型

### Component (组件)
```json
{
  "id": "string",
  "name": "string",
  "description": "string",
  "type": 0,
  "tags": "string",
  "file": "string",
  "size": 0,
  "hash": "string",
  "uploadTime": "string",
  "uploadUser": "string",
  "passTime": "string",
  "uploadUserName": "string",
  "typeName": "string",
  "downCount": 0,
  "version": "string",
  "apply": "string",
  "author": "string"
}
```

### QuestionBank (题库)
```json
{
  "id": "string",
  "name": "string",
  "problem": "string",
  "options": "string",
  "answer": 0,
  "difficulty": 0,
  "type": "string",
  "enable": 0
}
```

### Maps (地图)
```json
{
  "tx": 0,
  "img": "string",
  "csf": "string",
  "id": "string",
  "name": "string",
  "maxPlayers": "string",
  "author": "string",
  "type": 0,
  "base64": "string",
  "file": "string",
  "otherFile": "string",
  "createTime": "string",
  "updateTime": "string",
  "createUser": "string",
  "enable": 0,
  "downCount": "string",
  "score": 0.0,
  "description": "string",
  "sha1": "string",
  "uploadUserName": "string",
  "typeName": "string",
  "rules": "string",
  "ares": 0,
  "enemyHouse": "string",
  "allyHouse": "string",
  "autoStart": false
}
```

### User (用户)
```json
{
  "id": "string",
  "username": "string",
  "password": "string",
  "allow_time": "string",
  "email": "string",
  "role": "string",
  "tag": "string",
  "side": 3,
  "badge": "0",
  "mac": "string"
}
```

### Page<T> (分页响应)
```json
{
  "records": [],     // 当前页数据列表
  "total": 0,        // 总记录数
  "size": 0,         // 每页大小
  "current": 0,      // 当前页码
  "pages": 0         // 总页数
}
```

**完整API响应示例**:
```json
{
  "code": "200",
  "message": "获取成功",
  "data": {
    "records": [],
    "total": 0,
    "size": 0,
    "current": 0,
    "pages": 0
  }
}
```

---

## 错误处理

### 错误响应格式
```json
{
  "code": "400",
  "message": "用户名或密码错误",
  "data": null
}
```

**常见错误示例**:

1. **认证失败**
```json
{
  "code": "401",
  "message": "Token已过期，请重新登录",
  "data": null
}
```

2. **参数错误**
```json
{
  "code": "400",
  "message": "缺少必要参数: email",
  "data": null
}
```

3. **资源不存在**
```json
{
  "code": "404",
  "message": "地图不存在",
  "data": null
}
```

4. **服务器错误**
```json
{
  "code": "500",
  "message": "服务器内部错误",
  "data": null
}
```

### 常见错误码
- `网络错误`: 网络连接失败
- `HTTP 4xx`: 请求参数错误
- `HTTP 5xx`: 服务器内部错误

---

## 认证机制

### Token 使用
所有需要认证的接口都需要在请求头中添加:
```
Authorization: Bearer {access_token}
```

Token 在用户登录或注册后返回，客户端会将其保存在本地配置中 (`UserINISettings.Instance.Token.Value`)

---

## 外部服务集成

### CnCNet 隧道服务器
- **URL**: `https://ra2yr.dreamcloud.top/tunnel/list.txt`
- **格式**: 纯文本，每行一个服务器
- **字段**:
  ```
  address;country;countrycode;name;password;clients;maxclients;official;latitude;longitude;version;distance
  ```

### CnCNet 地图服务器
- **上传**: `http://mapdb.cncnet.org/upload`
- **下载**: `http://mapdb.cncnet.org/{game}/{sha1}.zip`

---

## 注意事项

1. **时间格式**: 所有时间字段使用字符串格式
2. **ID字段**: 大部分ID字段为字符串类型
3. **分页**: 分页索引从1开始
4. **文件上传**: 使用 `MultipartFormDataContent` 格式
5. **更新通道**: 0=稳定版(Stable), 1=测试版(Insiders)
6. **阵营**: 3=中立, 其他值对应不同阵营

---

## 客户端配置

客户端通过以下配置管理API连接:
- **配置文件**: `ClientCore/Settings/NetWorkINISettings.cs`
- **Token存储**: `UserINISettings.Instance.Token.Value`
- **更新服务器列表**: `Resources/Settings/Settings.ini` 或从API获取

---

生成日期: 2025-01-XX
基于源码版本: RF-Client Master Branch
