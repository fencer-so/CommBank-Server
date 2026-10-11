import json
import requests
from pymongo import MongoClient

# 1. 数据库配置
uri = (
    "mongodb://1832466501:qCma4OHYfcA523qT@"
    "ac-imaxjt1-shard-00-00.licv5ie.mongodb.net:27017,"
    "ac-imaxjt1-shard-00-01.licv5ie.mongodb.net:27017,"
    "ac-imaxjt1-shard-00-02.licv5ie.mongodb.net:27017/CommBank"
    "?ssl=true&replicaSet=atlas-n3oirm-shard-0&authSource=admin&appName=Cluster0"
)

DB_NAME = "CommBank"
GITHUB_OWNER = "Allen1832466501"
GITHUB_REPO = "CommBank-Server"

# 忽略配置类文件，只导入业务数据
IGNORED_KEYWORDS = ["appsettings", "launchsettings", "secrets", "package", "tsconfig"]

print("🔌 正在连接 MongoDB Atlas 数据库...")

try:
    client = MongoClient(uri, serverSelectionTimeoutMS=10000)
    db = client[DB_NAME]
    client.admin.command('ping')
    print("✅ 数据库成功连通！开始检索 GitHub 仓库...\n")
except Exception as e:
    print(f"❌ 数据库连接失败: {e}")
    exit(1)

def scan_repo_files(owner, repo, path=""):
    """使用 GitHub API 深度扫描所有目录下的 .json 文件"""
    api_url = f"https://api.github.com/repos/{owner}/{repo}/contents/{path}"
    response = requests.get(api_url, headers={"Accept": "application/vnd.github.v3+json"})
    json_files = []
    
    if response.status_code == 200:
        items = response.json()
        if isinstance(items, list):
            for item in items:
                if item["type"] == "file" and item["name"].endswith(".json"):
                    # 过滤配置类文件
                    if not any(key in item["name"].lower() for key in IGNORED_KEYWORDS):
                        json_files.append({
                            "name": item["name"],
                            "path": item["path"],
                            "download_url": item["download_url"]
                        })
                elif item["type"] == "dir":
                    # 递归遍历子文件夹
                    json_files.extend(scan_repo_files(owner, repo, item["path"]))
    return json_files

print(f"🚀 开始全盘深度检索仓库 [{GITHUB_OWNER}/{GITHUB_REPO}]...")
json_files = scan_repo_files(GITHUB_OWNER, GITHUB_REPO)

if not json_files:
    print("⚠️ 依然未找到业务数据 JSON 文件！")
    print("👉 请检查本地文件是否已执行 git push，或者 JSON 文件是否在其它分支。")
else:
    print(f"🔍 成功搜寻到 {len(json_files)} 个业务 JSON 文件，开始写入数据库:\n")
    for file_info in json_files:
        collection_name = file_info["name"].replace(".json", "").lower()
        try:
            res = requests.get(file_info["download_url"])
            if res.status_code == 200:
                # 自动去除 UTF-8 BOM 头
                text_content = res.content.decode('utf-8-sig')
                data = json.loads(text_content)
                
                if isinstance(data, dict):
                    data = [data]
                if isinstance(data, list) and len(data) > 0:
                    collection = db[collection_name]
                    collection.delete_many({})
                    result = collection.insert_many(data)
                    print(f"🎉 集合 [{collection_name}] (路径: {file_info['path']}) 写入成功！导入 {len(result.inserted_ids)} 条记录。")
                else:
                    print(f"⚠️ 文件 [{file_info['name']}] 内容为空。")
        except Exception as e:
            print(f"❌ 集合 [{collection_name}] 解析/导入失败: {e}")

print("\n✨ 处理完成！去 Compass 刷新查看！")
